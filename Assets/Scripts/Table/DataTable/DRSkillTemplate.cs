//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：
//------------------------------------------------------------

using GameFramework;
using UnityGameFramework.Runtime;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Table
{
    /// <summary>
    /// 这是文本
    /// </summary>
    public class DRSkillTemplate : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取唯一Id索引。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取唯一Key索引。
        /// </summary>
        public string KeyName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取模板名字。
        /// </summary>
        public string ClassName
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            m_Id = int.Parse(columnStrings[index++]);
            KeyName = columnStrings[index++];
            ClassName = columnStrings[index++];

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    KeyName = binaryReader.ReadString();
                    ClassName = binaryReader.ReadString();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}
