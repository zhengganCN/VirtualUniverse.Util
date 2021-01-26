using System;

namespace VirtualUniverse.Repository.SqlSugarTest.Models
{
    public class TbAnswer
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Guid? QuestionId { get; set; }
        public string Answer { get; set; }
        public Guid? UserId { get; set; }
    }
}