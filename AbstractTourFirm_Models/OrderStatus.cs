using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm_Models
{
    public enum OrderStatus
    {
        Рассматривается = 0,
        Готов_и_ждёт_оплаты = 1,
        В_кредите = 2,
        Оплачен = 3
    }
}
