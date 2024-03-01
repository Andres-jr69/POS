using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infractructure.Commons.Bases
{
    //Esta clase nos va ayudar a nosostros a devolver el registro, los registro desde la base de datos
    public class BaseEntityResponse<T>
    {
        public int? TotalRecords { get; set; }
        public List<T>? Items { get; set; }
    }
}
