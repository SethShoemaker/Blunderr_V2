using System.Text;

namespace Blunderr.Core.Services.PhoneService
{
    public class PhoneService : IPhoneService
    {
        public string Format(int phone)
        {
            StringBuilder sb = new(12);

            for (int i = 11; i > -1; i--)
            {
                if(i is 7 or 3){
                    sb.Insert(0, '-');
                    continue;
                }

                int lastDigit = phone % 10;
                sb.Insert(0, lastDigit);

                phone = (int)Math.Round(phone / 10, decimals: 0);
            }
            
            return sb.ToString();
        }
    }
}