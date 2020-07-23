using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channelstatics.Models
{
    public class Channel
    {
        public string ChannelName { get; set; }
        public string Title { get; set; }
        public int? Subscribers { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string CountPost { get; set; }
        public int IdChannel { get; set; }

    }
}
