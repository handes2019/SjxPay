using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SjxPay.Domain
{
    public class AuditEntity : Entity, IModifiedOn, ICreatedOn, IDeleteOn
    {
        /// <summary>
        /// 修改人
        /// </summary>
        public ModifiedBy ModifiedBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedOn { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 冗余名称
        /// </summary>
        public string CreatorName { get; set; }
        /// <summary>
        /// 冗余头像
        /// </summary>
        public string CreatorHeader { get; set; }
        public DateTime? DeleteOn { get; set; }
    }

    public interface IDeleteOn
    {
        public DateTime? DeleteOn { get; set; }
    }
}
