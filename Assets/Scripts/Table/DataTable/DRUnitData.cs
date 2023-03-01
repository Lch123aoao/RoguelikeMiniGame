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
    public class DRUnitData : DataRowBase
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
        /// 获取实体资源id。
        /// </summary>
        public int EntityId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取单位名称。
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取血量。
        /// </summary>
        public int HP
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取经验值。
        /// </summary>
        public int EXP
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取单位品阶。
        /// </summary>
        public int Grade
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取初始持有技能。
        /// </summary>
        public string SkillList
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
            EntityId = int.Parse(columnStrings[index++]);
            Name = columnStrings[index++];
            HP = int.Parse(columnStrings[index++]);
            EXP = int.Parse(columnStrings[index++]);
            Grade = int.Parse(columnStrings[index++]);
            SkillList = columnStrings[index++];

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
                    EntityId = binaryReader.Read7BitEncodedInt32();
                    Name = binaryReader.ReadString();
                    HP = binaryReader.Read7BitEncodedInt32();
                    EXP = binaryReader.Read7BitEncodedInt32();
                    Grade = binaryReader.Read7BitEncodedInt32();
                    SkillList = binaryReader.ReadString();
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
