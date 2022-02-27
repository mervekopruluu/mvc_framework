using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities.Concrete
{
    public class BaseEntity:IEntity
    {
        public int Id { get; set; }

    }
}