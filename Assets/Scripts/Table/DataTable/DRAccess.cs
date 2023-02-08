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
    /// 获取途径
    /// </summary>
    public class DRAccess : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取id。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取类型。
        /// </summary>
        public int Type
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取参数。
        /// </summary>
        public string Parameter
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取描述1。
        /// </summary>
        public string Describe1
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取描述2。
        /// </summary>
        public string Describe2
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
            Type = int.Parse(columnStrings[index++]);
            Parameter = columnStrings[index++];
            Describe1 = columnStrings[index++];
            Describe2 = columnStrings[index++];

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
                    Type = binaryReader.Read7BitEncodedInt32();
                    Parameter = binaryReader.ReadString();
                    Describe1 = binaryReader.ReadString();
                    Describe2 = binaryReader.ReadString();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private KeyValuePair<int, string>[] m_Describe = null;

        public int DescribeCount
        {
            get
            {
                return m_Describe.Length;
            }
        }

        public string GetDescribe(int id)
        {
            foreach (KeyValuePair<int, string> i in m_Describe)
            {
                if (i.Key == id)
                {
                    return i.Value;
                }
            }

            throw new GameFrameworkException(Utility.Text.Format("GetDescribe with invalid id '{0}'.", id.ToString()));
        }

        public string GetDescribeAt(int index)
        {
            if (index < 0 || index >= m_Describe.Length)
            {
                throw new GameFrameworkException(Utility.Text.Format("GetDescribeAt with invalid index '{0}'.", index.ToString()));
            }

            return m_Describe[index].Value;
        }

        private void GeneratePropertyArray()
        {
            m_Describe = new KeyValuePair<int, string>[]
            {
                new KeyValuePair<int, string>(1, Describe1),
                new KeyValuePair<int, string>(2, Describe2),
            };
        }
    }
}
