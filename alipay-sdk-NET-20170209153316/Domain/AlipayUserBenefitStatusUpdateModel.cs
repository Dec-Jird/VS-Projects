using System;
using System.Xml.Serialization;

namespace Aop.Api.Domain
{
    /// <summary>
    /// AlipayUserBenefitStatusUpdateModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayUserBenefitStatusUpdateModel : AopObject
    {
        /// <summary>
        /// 权益的ID
        /// </summary>
        [XmlElement("benefit_id")]
        public string BenefitId { get; set; }

        /// <summary>
        /// 1:上线， 0:下线,  2:失效；  上线状态所有人可见，不可编辑；  下线状态白名单可见，可以编辑；  失效状态所有人不可见，不可编辑。
        /// </summary>
        [XmlElement("benefit_status")]
        public string BenefitStatus { get; set; }
    }
}