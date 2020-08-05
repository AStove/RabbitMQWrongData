using System;
using System.Collections.Generic;
using System.Text;

namespace MessageSender
{
    public class DigitCreation
    {
        private readonly MyMessagingClient myMessagingClient;

        public DigitCreation(MyMessagingClient myMessagingClient)
        {
            this.myMessagingClient = myMessagingClient;
        }

        public void Start()
        {
            var i = 0;
            while (i < 10)
            {
                Console.WriteLine($"Sending {i}");
                myMessagingClient.SendDigit(i++);
            }
        }
    }
}
