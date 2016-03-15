using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A11_RBS.Domain
{
    public class Key
    {
      public Key()
      {
          this.Id = Guid.NewGuid();
      }
        public Guid Id { get; set; }
    }
}