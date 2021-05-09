using Coin51_chia.Common;
using Plaisted.PowershellHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coin51_chia.Models
{
    public class ChiaSetting
    {
        /// <summary>
        /// chia安装目录
        /// </summary>
        public string setupPath { get; set; }

        /// <summary>
        /// 矿工公匙
        /// </summary>
        public string farmerPublicKey { get; set; }

        /// <summary>
        /// 矿池公匙
        /// </summary>
        public string poolPublicKey { get; set; }

        /// <summary>
        /// 耕种配置
        /// </summary>
        public List<PoltConfig> poltConfig { get; set; }

        /// <summary>
        /// 保存配置到指定位置
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public static void SaveConfig(ChiaSetting model, string fileNmae = "config.json")
        {
            try
            {
                if (!File.Exists(fileNmae))
                {
                    var temp = File.Create(fileNmae);
                    temp.Flush();
                    temp.Close();
                }
                File.WriteAllText(fileNmae, Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.UTF8);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ChiaSetting LoadConfig(string fileNmae = "config.json")
        {
            try
            {
                if (File.Exists(fileNmae))
                {
                    var info = File.ReadAllText(fileNmae, Encoding.UTF8);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ChiaSetting>(info);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return new ChiaSetting() { poltConfig = new List<PoltConfig>() };
        }


        /// <summary>
        /// 命令获取当前系统的公钥信息
        /// </summary>
        public void GatPublicKeys()
        {
            if (string.IsNullOrWhiteSpace(poolPublicKey) || string.IsNullOrWhiteSpace(farmerPublicKey))
            {
                using (var helper = new PowershellHelper(LogerHelper.loggerFactory).WithOptions(o => { o.CleanupMethod = CleanupType.RecursiveAdmin; }))
                {
                    helper.AddInputObject("resultData", new { result = "" });
                    helper.AddCommand($"$resultData.result = {setupPath} keys show | Out-String");
                    helper.AddOutputObject("resultData");

                    var result = Task.Run(() => helper.RunAsync(new System.Threading.CancellationToken()));
                    var _ret = result.Result;
                    var scriptOutput = helper.Output["resultData"].ToObject<dynamic>();
                    string _retStr = scriptOutput.result;
                    var keys = _retStr?.Split(System.Environment.NewLine.ToCharArray())
                        .ToList()?
                        .Where(w1 => !string.IsNullOrEmpty(w1))?
                        .ToDictionary(key => key?.Split(':')?.FirstOrDefault()?.Trim(), value => value?.Split(':')?.LastOrDefault()?.Trim());

                    poolPublicKey = keys?.FirstOrDefault(f1 => f1.Key.ToLower().Contains("pool") && f1.Key.ToLower().Contains("public")).Value;
                    farmerPublicKey = keys?.FirstOrDefault(f1 => f1.Key.ToLower().Contains("farmer") && f1.Key.ToLower().Contains("public")).Value;
                    helper.WaitOnCleanup();
                }
            }
        }
    }
}
