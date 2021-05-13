using System;
using System.Diagnostics;
using System.IO;
using System.Text;


namespace Coin51_chia.Common
{
    /// <summary>
    /// This class should aid in executing processes and handling output of those processes with redirection modes.
    ///
    /// <para/>
    /// REDIRECTION MODES:
    /// <list type="bullet">
    /// <item>
    ///     <term> None </term>
    ///     <description> <para/> No redirection. The process is simply executed. </description>
    /// </item>
    /// <item>
    ///      <term> UseHandlers </term>
    ///      <description> <para/> Assign <see cref="StdoutHandler"/> and <see cref="StderrHandler"/> to handle stdout and stderr
    ///     of the process respectively. </description>
    /// </item>
    /// <item>
    ///     <term> RedirectStreams </term>
    ///     <description>
    ///     <para/>
    ///     Redirects the output to stream readers of type <see cref="StreamReader"/>.
    ///         Use <see cref="StdoutReader"/> and <see cref="StderrHandler"/>
    ///         to get stdout and stderr of the process respectively.
    ///     </description>
    /// </item>
    /// <item>
    ///    <term> RedirectStdout </term>
    ///    <description> <para/> Redirects only the process stdout to <see cref="StdoutReader"/>. </description>
    /// </item>
    ///     <term> RedirectStderr </term>
    ///     <description> <para/> Redirects only the process stderr to <see cref="StderrReader"/>. </description>
    /// <item>
    ///     <term> StdoutHandler </term>
    ///     <description><para/> Handle only process stdout. Must assign  <see cref="StdoutHandler"/>. </description>
    /// </item>
    /// <item>
    ///     <term> StderrHandler </term>
    ///     <description><para/> Handle only process stderr. Must assign  <see cref="StderrHandler"/>. </description>
    /// </item>
    /// <item>
    ///     <term> StdoutStreamWithStderrHandler </term>
    ///     <description><para/> stdout redirected to <see cref="StdoutReader"/>. <see cref="StderrHandler"/>
    ///     must be assigned to handle stderr. </description>
    /// </item>
    /// <item>
    ///     <term> StderrStreamWithStdoutHandler </term>
    ///     <description> <para/> stderr redirected to <see cref="StderrReader"/>. <see cref="StdoutHandler"/>
    ///     must be assigned to handle stdout.
    ///     </description>
    /// </item>
    /// </list>
    /// <para/>
    /// <remarks>
    ///  If the executable exists then it is executed. If not then the executable in PATH is executed.
    ///  It no such executable is found it fails.
    /// </remarks>
    /// 
    /// </summary>
    ///
    /// <para/><para/>
    ///
    /// 
    ///  USAGE EXAMPLE:
    ///  <code>
    ///  var executor = new ProcessExecutor("/path/to/process")
    ///    {
    ///        WaitForExit = true, // When executing waits for process to execute. Optional and can be done by the user.
    ///        StdoutHandler = (object sender, <see cref="DataReceivedEventArgs"/> e) => { Console.WriteLine(e.Data); }
    ///        StderrHandler = MyStdErrHandler // A method with arguments object and DataReceivedEventArgs
    ///    };
    ///
    ///  //by default RedirectionMode <see cref="Mode"/> is None and process will be simply executed.
    ///  Process p = executor.Execute();
    ///
    ///  executor.Mode = ProcessExecutor.RedirectionMode.UseHandlers;
    ///  executor.Execute(); // now stderr and stdout of process will be handled 
    ///                      // by StdoutHandler and StderrHandler delegates respectively
    /// 
    ///  executor.Mode = ProcessExecutor.RedirectionMode.RedirectStreams;
    ///  executor.Execute();
    ///  Console.WriteLine(executor.<see cref="StdoutReader"/>.ReadToEnd()); // All output is read since <see cref="WaitForExit"/> is true>
    ///  Console.WriteLine(executor.<see cref="StderrReader"/>.ReadToEnd());
    ///  </code>

    public class ProcessExecutor
    {
        
        public enum RedirectionMode
        {
            None,
            UseHandlers,
            RedirectStreams,
            RedirectStdout,
            RedirectStderr,
            StdoutHandler,
            StderrHandler,
            RedirectStdoutWithStderrHandler,
            RedirectStderrWithStdoutHandler
        }

        public string ExecutablePath { get; set; }
        public virtual string[] Args { get; set; } = new string[0];
        public RedirectionMode Mode { get; set; } = RedirectionMode.None;
        public bool WaitForExit { get; set; } = false;
        public bool Verbose { get; set; }

        public DataReceivedEventHandler StdoutHandler { get; set; }
        public DataReceivedEventHandler StderrHandler { get; set; }
        
        public EventHandler ExitHandler { get; set; }

        public StreamReader StdoutReader { get; private set; }
        public StreamReader StderrReader { get; private set; }

        public ProcessExecutor(string executablePath)
        {
            ExecutablePath = executablePath;
        }

        public virtual Process Execute()
        {
            return Execute(Mode, Args);
        }

        protected virtual Process Execute(RedirectionMode mode, params string[] args)
        {
            Process p = null;
            StreamReader stdoutTempReader;
            StreamReader stderrTempReader;
            switch (Mode)
            {
                case RedirectionMode.None:
                    p = ExecuteProcess(ExecutablePath, ExitHandler, WaitForExit, args);
                    break;
                case RedirectionMode.UseHandlers:
                    if (StdoutHandler == null || StderrHandler == null)
                        throw new InvalidOperationException(
                            "With mode UseHandlers, both handlers should be assigned! \n "
                            + (StdoutHandler == null ? "StdoutHandler is not assigned\n" : "")
                            + (StderrHandler == null ? "StdoutErrHandler is not assigned\n" : ""));
                    p = ExecuteProcess(ExecutablePath, StdoutHandler, StderrHandler, ExitHandler, WaitForExit, args);
                    break;
                case RedirectionMode.RedirectStreams:
                    p = ExecuteProcess(ExecutablePath, out stdoutTempReader, out stderrTempReader, ExitHandler, WaitForExit, args);
                    StdoutReader = stdoutTempReader;
                    StderrReader = stderrTempReader;
                    break;
                case RedirectionMode.RedirectStdoutWithStderrHandler:
                    if (StderrHandler == null)
                        throw new InvalidOperationException(
                            "With mode StdoutStreamWithStderrHandler, StdErrHandler should be assigned! \n");
                    p = ExecuteProcess(ExecutablePath, true, out stdoutTempReader, StderrHandler, ExitHandler, WaitForExit, args);
                    StdoutReader = stdoutTempReader;
                    break;
                case RedirectionMode.RedirectStderrWithStdoutHandler:
                    if (StderrHandler == null)
                        throw new InvalidOperationException(
                            "With mode StderrStreamWithStdoutHandler, StdoutHandler should be assigned! \n");
                    p = ExecuteProcess(ExecutablePath, false, out stderrTempReader, StdoutHandler, ExitHandler, WaitForExit, args);
                    StderrReader = stderrTempReader;
                    break;
                case RedirectionMode.RedirectStdout:
                    p = ExecuteProcess(ExecutablePath, true, out stdoutTempReader, ExitHandler, WaitForExit, args);
                    StdoutReader = stdoutTempReader;
                    break;
                case RedirectionMode.RedirectStderr:
                    p = ExecuteProcess(ExecutablePath, false, out stderrTempReader, ExitHandler, WaitForExit, args);
                    StderrReader = stderrTempReader;
                    break;
                case RedirectionMode.StdoutHandler:
                    if (StdoutHandler == null)
                        throw new InvalidOperationException(
                            "With mode StdoutHandler, StdoutHandler should be assigned! \n");
                    p = ExecuteProcess(ExecutablePath, true,  StdoutHandler, ExitHandler, WaitForExit, args);
                    break;
                case RedirectionMode.StderrHandler:
                    if (StderrHandler == null)
                        throw new InvalidOperationException(
                            "With mode StderrHandler, StderrHandler should be assigned! \n");
                    p = ExecuteProcess(ExecutablePath, false,  StderrHandler, ExitHandler, WaitForExit, args);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return p;
        }

        /// <summary>
        /// Execute process with redirecting output.
        /// </summary>
        /// <param name="pathToExecutable"></param>
        /// <param name="stdout"></param>
        /// <param name="stderr"></param>
        /// <param name="exitHandler"></param>
        /// <param name="waitForExit"></param>
        /// <param name="args"></param>
        public static Process ExecuteProcess(string pathToExecutable,
            out StreamReader stdout,
            out StreamReader stderr,
            EventHandler exitHandler,
            bool waitForExit = false,
            params string[] args)
        {
            return ProcessExecutor.ExecuteProcess(
                pathToExecutable,
                true,
                true,
                out stdout,
                out stderr,
                null,
                null,
                exitHandler,
                waitForExit,
                args);
        }

        /// <summary>
        /// Execute process with redirecting only one stream.
        /// </summary>
        /// <param name="pathToExecutable"></param>
        /// <param name="redirectStdOut"></param>
        /// <param name="outStreamReader"> The streamReader for captured stdout or stderr</param>
        /// <param name="exitHandler"></param>
        /// <param name="waitForExit"></param>
        /// <param name="args"></param>
        public static Process ExecuteProcess(string pathToExecutable,
            bool redirectStdOut,
            out StreamReader outStreamReader,
            EventHandler exitHandler,
            bool waitForExit = false,
            params string[] args)
        {
            StreamReader _;
            return redirectStdOut
                ? ProcessExecutor.ExecuteProcess(
                    pathToExecutable,
                    true,
                    false,
                    out outStreamReader,
                    out _,
                    null,
                    null,
                    exitHandler,
                    waitForExit,
                    args)
                : ProcessExecutor.ExecuteProcess(
                    pathToExecutable,
                    false,
                    true,
                    out _,
                    out outStreamReader,
                    null,
                    null,
                    exitHandler,
                    waitForExit,
                    args);
        }

        /// <summary>
        /// Execute a process without capturing output
        /// </summary>
        /// <param name="pathToExecutable"></param>
        /// <param name="waitForExit"></param>
        /// <param name="args"></param>
        public static Process ExecuteProcess(string pathToExecutable, EventHandler exitHandler, bool waitForExit = false, params string[] args)
        {
            StreamReader _;
            return
                ProcessExecutor.ExecuteProcess(
                    pathToExecutable,
                    false,
                    false,
                    out _,
                    out _,
                    null,
                    null,
                    exitHandler,
                    waitForExit,
                    args);
        }

        public static Process ExecuteProcess(string pathToExecutable,
            DataReceivedEventHandler stdoutHandler,
            DataReceivedEventHandler stderrHandler,
            EventHandler exitHandler = null,
            bool waitForExit = false, params string[] args)
        {
            StreamReader _;
            return
                ProcessExecutor.ExecuteProcess(
                    pathToExecutable,
                    false,
                    false,
                    out _,
                    out _,
                    stdoutHandler,
                    stderrHandler,
                    exitHandler,
                    waitForExit,
                    args);
        }

        /// <summary>
        /// Can only have stdout stream and stderr handler or
        /// stderr stream and stdout handler because synchronous and
        /// async functions can not be mixed.
        /// </summary>
        /// <param name="pathToExecutable"></param>
        /// <param name="captureStdoutStreamAndUseErrHandler">
        ///     True to have stdout as a stream and handle stderr with a handler.
        ///     False for the other way around
        /// </param>
        /// <param name="outStreamReader"></param>
        /// <param name="handler"></param>
        /// <param name="exitHandler"></param>
        /// <param name="waitForExit"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Process ExecuteProcess(string pathToExecutable,
            bool captureStdoutStreamAndUseErrHandler,
            out StreamReader outStreamReader,
            DataReceivedEventHandler handler,
            EventHandler exitHandler,
            bool waitForExit = false,
            params string[] args)
        {
            StreamReader _;
            return captureStdoutStreamAndUseErrHandler
                ? ProcessExecutor.ExecuteProcess(
                    pathToExecutable,
                    true,
                    false,
                    out outStreamReader,
                    out _,
                    null,
                    handler,
                    exitHandler,
                    waitForExit,
                    args)
                : ProcessExecutor.ExecuteProcess(
                    pathToExecutable,
                    false,
                    true,
                    out _,
                    out outStreamReader,
                    handler,
                    null,
                    exitHandler,
                    waitForExit,
                    args);
        }

        /// <summary>
        /// The most complete execute process other overloads are based on.
        /// </summary>
        /// <param name="pathToExecutable"></param>
        /// <param name="redirectStdOut"></param>
        /// <param name="redirectStdErr"></param>
        /// <param name="stdout"></param>
        /// <param name="stderr"></param>
        /// <param name="stdoutHandler"></param>
        /// <param name="stderrHandler"></param>
        /// <param name="exitHandler"></param>
        /// <param name="waitForExit"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private static Process ExecuteProcess(
            string pathToExecutable,
            bool redirectStdOut,
            bool redirectStdErr,
            out StreamReader stdout,
            out StreamReader stderr,
            DataReceivedEventHandler stdoutHandler = null,
            DataReceivedEventHandler stderrHandler = null,
            EventHandler exitHandler = null,
            bool waitForExit = false,
            params string[] args)
        {
            if (redirectStdOut && stdoutHandler != null)
            {
                throw new InvalidOperationException(
                    "Can not redirect stdout and have a handler as well because MSDN says that " +
                    "asynchronous and synchronous reading of stream should not occur\n " +
                    "Reason: redirectStdOut == true && stdoutHandler != null ");
            }

            if (redirectStdErr && stderrHandler != null)
            {
                throw new InvalidOperationException(
                    "Can not redirect stdout and have a handler as well because MSDN says that " +
                    "asynchronous and synchronous reading of stream should not occur\n " +
                    "Reason: redirectStdErr && stderrHandler != null ");
            }

            var argsSb = new StringBuilder();

            foreach (var arg in args)
            {
                argsSb.Append($"{arg} ");
            }

            var noRedirection = !redirectStdOut && !redirectStdErr
                                                && stdoutHandler == null && stderrHandler == null;
            Console.WriteLine(Path.GetFullPath(pathToExecutable));

            if (File.Exists(pathToExecutable))
            {
                pathToExecutable = Path.GetFullPath(pathToExecutable);
            }
            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = pathToExecutable,
                    UseShellExecute = noRedirection,
                    RedirectStandardOutput = redirectStdOut | stdoutHandler != null,
                    RedirectStandardError = redirectStdErr | stderrHandler != null,
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    Arguments = argsSb.ToString(),
                    WindowStyle= ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };

            // Manage handlers if assigned
            if (stderrHandler != null)
                p.ErrorDataReceived += stderrHandler;

            if (stdoutHandler != null)
                p.OutputDataReceived += stdoutHandler;

            if (exitHandler != null)
                p.Exited += exitHandler;

            p.Start();

            // Redirect the streams if assigned
            if (!noRedirection)
                p.EnableRaisingEvents = true;

            if (stdoutHandler != null)
                p.BeginOutputReadLine();

            if (stderrHandler != null)
                p.BeginErrorReadLine();


            stdout = stderr = null;

            if (redirectStdOut)
                stdout = p.StandardOutput;

            if (redirectStdErr)
                stderr = p.StandardError;

            if (waitForExit)
            {
                p.WaitForExit();

                if (stdoutHandler != null)
                    p.OutputDataReceived -= stderrHandler;
                if (stderrHandler != null)
                    p.ErrorDataReceived -= stderrHandler;
            }

            return p;
        }

        public static void ExecutePythonScript(
            string scriptPath,
            string pythonExecutablePath = "python.exe",
            params string[] args)
        {
            using (var pythonExecuteProcess = new Process())
            {
                Console.WriteLine("===============");
                Console.WriteLine("Executing python script ... ");

                scriptPath = $"\"{scriptPath}\" ";
                scriptPath = scriptPath.Replace("/", @"\");

                pythonExecutablePath = $"\"{pythonExecutablePath}\" ";

                pythonExecuteProcess.StartInfo.FileName = pythonExecutablePath;

                // Python script and Arguments
                var argsSb = new StringBuilder(scriptPath);
                foreach (var arg in args)
                    argsSb.Append($"{arg} ");
                argsSb.AppendLine();

                pythonExecuteProcess.StartInfo.Arguments = argsSb.ToString();

                pythonExecuteProcess.StartInfo.UseShellExecute = false;
                pythonExecuteProcess.StartInfo.RedirectStandardOutput = true;
                pythonExecuteProcess.StartInfo.RedirectStandardError = true;
                pythonExecuteProcess.Start();

                var stdout = pythonExecuteProcess.StandardOutput.ReadToEnd();
                var stderr = pythonExecuteProcess.StandardError.ReadToEnd();
                pythonExecuteProcess.WaitForExit();


                Console.WriteLine("Used python:  " + pythonExecutablePath);
                Console.WriteLine("Used script Path: " + scriptPath);
                Console.WriteLine("Used script:  " + pythonExecuteProcess);
                Console.WriteLine("Used args  :  " + argsSb);
                Console.WriteLine($"Exit code : {pythonExecuteProcess.ExitCode}");
                Console.WriteLine($"Stdout : {stdout}");
                Console.WriteLine($"Stderr : {stderr}");
            }
        }

        public static Process ExecuteProcess(string pathToExecutable,
            bool stdoutHandler,
            DataReceivedEventHandler handler,
            EventHandler exitHandler,
            bool waitForExit,
            string[] args)
        {
            StreamReader _;
            return stdoutHandler
                ? ExecuteProcess(
                    pathToExecutable,
                    false,
                    false,
                    out _,
                    out _,
                    handler,
                    null,
                    exitHandler,
                    waitForExit,
                    args)
                : ExecuteProcess(
                    pathToExecutable,
                    false,
                    false,
                    out _,
                    out _,
                    null,
                    handler,
                    exitHandler,
                    waitForExit,
                    args);
        }
    }
}