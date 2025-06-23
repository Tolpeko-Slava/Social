using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Models
{
    public class FriendShow
    {
        public int ID { get; set; }
        public string email { get; set; }

        public FriendShow(int id, string email)
        {
            this.ID = id;
            this.email = email;
        }
    }
}
