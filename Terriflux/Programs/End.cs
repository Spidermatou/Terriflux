using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terriflux.Programs
{
    public record End
    {
        private enum EndKind { VICTORY, FAIL }

        private readonly Dictionary<EndKind, string> ends;

        public End() 
        {
            this.ends = new();

            this.ends.Add(EndKind.VICTORY, "Félicitations ! Vous avez réussi à équilibrer l'écologie, l'économie et le social. " +
            "La région prospère grâce à vos efforts acharnés. Vous êtes un véritable champion de la reterritorialisation, " +
            "démontrant que l'harmonie entre ces trois piliers est la clé d'un avenir durable.");

            this.ends.Add(EndKind.FAIL, "Votre quête pour la reterritorialisation a pris fin. Bien que vous ayez eu un impact " +
                "significatif sur la région, la situation est critique. L'un des piliers essentiels - l'écologie, " +
                "l'économie ou le social - est maintenant en péril, mettant en danger l'équilibre que vous avez tenté " +
                "de maintenir. Utilisez cette expérience pour réfléchir à de nouvelles stratégies, car même des pas " +
                "modestes dans la bonne direction peuvent avoir un effet profond et durable. C'est une leçon précieuse " +
                "pour vos futures entreprises. Continuez à bâtir sur ces fondations et à inspirer le changement positif.");
        }     
        
        public string Victory()
        {
            return this.ends[EndKind.VICTORY];
        }

        public string Fail()
        {
            return this.ends[EndKind.FAIL];
        }
    }
}
