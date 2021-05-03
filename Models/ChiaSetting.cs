using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coin51_chia.Entity
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

    }
}
