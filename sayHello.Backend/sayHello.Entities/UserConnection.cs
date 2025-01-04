using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayHello.Entities
{
    public class UserConnection
    {
         public int Id { get; set; }
        public string ConnectionId { get; set; }
        public string UserId { get; set; }
        public string ChatRoom { get; set; }
        
    }
}
