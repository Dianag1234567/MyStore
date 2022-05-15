using System;
using System.Collections.Generic;

#nullable disable

namespace MyStore.Domain.Entities
{
    public partial class Test
    {
        public Test()
        {
            Scores = new HashSet<Score>();
        }

        public string Testid { get; set; }

        public virtual ICollection<Score> Scores { get; set; }
    }
}
