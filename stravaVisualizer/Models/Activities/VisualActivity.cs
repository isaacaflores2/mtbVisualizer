using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StravaVisualizer.Models.Activities
{
    public class VisualActivity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long? ActivityId { get; set; }

        public float? StartLat { get; set; }
        public float? StartLong { get; set; }

        public float? EndLat { get; set; }
        public float? EndLong { get; set; }

        public string TrailName { get; set; }
        
        public SummaryActivity Summary { get; set;}
    
        public int? UserId { get; set; }

        public VisualActivity(SummaryActivity summaryActivity) : this()
        {
            Summary = summaryActivity;
            ActivityId = Summary.Id;

            if (Summary.StartLatlng != null)
            {
                StartLat = Summary.StartLatlng[0];
                StartLong = Summary.StartLatlng[1];
            }
            
            if(Summary.EndLatlng != null)
            {
                EndLat = Summary.EndLatlng[0];
                EndLong = Summary.EndLatlng[1];
            }
            
            UserId = Summary.Athlete.Id;
        }

        public VisualActivity()
        {
            TrailName = null;
        }
    }
}
