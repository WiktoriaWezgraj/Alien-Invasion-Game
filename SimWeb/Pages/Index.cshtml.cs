using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimWeb.Pages
{
    public class IndexModel : PageModel
    {
        public class PrivacyModel : PageModel
        {
            private const int DEFAULT_TURN = 1; // Domyœlna wartoœæ dla Turn
            private const int MAX_TURN = 20;   // Opcjonalnie: maksymalna liczba tur

            public int Turn { get; private set; }

            public void OnGet()
            {
                Turn = HttpContext.Session.GetInt32("Turn") ?? DEFAULT_TURN;
            }

            public void OnPost()
            {
                Turn = HttpContext.Session.GetInt32("Turn") ?? DEFAULT_TURN;

                Turn++;

                if (Turn > MAX_TURN)
                {
                    Turn = MAX_TURN;
                }

                HttpContext.Session.SetInt32("Turn", Turn);
            }
        }

    }
}
