using System;

namespace CHLeMudra.Entity
{

    public abstract class BaseEntity
    {
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        private DateTime _CreatedOn;
        public DateTime CreatedOn
        {
            get
            {
                if (_CreatedOn == DateTime.MinValue)
                    return DateTime.Now;
                return _CreatedOn;
            }
            set
            {
                _CreatedOn = value;
            }
        }
        private DateTime _ModifiedOn;
        public DateTime ModifiedOn
        {
            get
            {
                return _ModifiedOn = DateTime.Now;
            }
            set
            {
                _ModifiedOn = value;
            }
        }
        
        
    }
}
