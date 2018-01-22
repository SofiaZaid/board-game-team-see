using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Player
    {
        public string NickName { get; set; }
        public int PlayerID { get; set; }
        public int MarkId { get; set; } 
        public int GamePieces { get; set; }
    }
}
