using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TP3
{
        enum TypeBloc
        {
            NONE,
            FROZEN,
            SQUARE,
            LINE,
            T,
            L,
            J,
            S,
            Z,
        }
        enum Mouvement
        {
            Mouvement_Descendre,
            Mouvement_Gauche,
            Mouvement_Droite,
            Mouvement_RotationDroite,
            Mouvement_RotationGauche

        }
    }
}
