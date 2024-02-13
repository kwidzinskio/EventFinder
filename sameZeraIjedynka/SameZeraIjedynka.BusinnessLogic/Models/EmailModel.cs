using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameZeraIjedynka.BusinnessLogic.Models
{
    public class EmailModel
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = "" +
            "<h1>" +
                "Dziękujemy za rejestrację" +
            "</h1>" +
            "<br>" +
            "By zobaczyć nasze wspaniałe eventy przejdź tutaj: " +
            "<a href=\"https://localhost:7000/Event/Index\">" +
                "<button style=\"background-color: #4CAF50; border: none; color: white; padding: 10px 20px; text-align: center; text-decoration: none; display: inline-block; font-size: 16px; border-radius: 5px; cursor: pointer; \"> " +
                            "Click" +
                "</button>" +
            "</a>";
}
}
