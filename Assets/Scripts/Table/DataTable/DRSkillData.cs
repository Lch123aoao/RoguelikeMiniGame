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
    public class DRSkillData : DataRowBase
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
        /// 获取skill实体id。
        /// </summary>
        public int EntityId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取技能模板id。
        /// </summary>
        public int SkillTemplateId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取朝向类型。
        /// </summary>
        public int TowardType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取攻击力。
        /// </summary>
        public int AttackValue
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取生成技能间隔。
        /// </summary>
        public int SkillCD
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取攻击间隔/CD。
        /// </summary>
        public int AttackCD
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取基本移动速度。
        /// </summary>
        public int BaseMoveSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取作用次数。
        /// </summary>
        public int UsageCount
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取持续时间。
        /// </summary>
        public int Duration
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
            SkillTemplateId = int.Parse(columnStrings[index++]);
            TowardType = int.Parse(columnStrings[index++]);
            AttackValue = int.Parse(columnStrings[index++]);
            SkillCD = int.Parse(columnStrings[index++]);
            AttackCD = int.Parse(columnStrings[index++]);
            BaseMoveSpeed = int.Parse(columnStrings[index++]);
            UsageCount = int.Parse(columnStrings[index++]);
            Duration = int.Parse(columnStrings[index++]);

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
                    SkillTemplateId = binaryReader.Read7BitEncodedInt32();
                    TowardType = binaryReader.Read7BitEncodedInt32();
                    AttackValue = binaryReader.Read7BitEncodedInt32();
                    SkillCD = binaryReader.Read7BitEncodedInt32();
                    AttackCD = binaryReader.Read7BitEncodedInt32();
                    BaseMoveSpeed = binaryReader.Read7BitEncodedInt32();
                    UsageCount = binaryReader.Read7BitEncodedInt32();
                    Duration = binaryReader.Read7BitEncodedInt32();
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
