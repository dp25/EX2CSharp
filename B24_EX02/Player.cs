using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B24_EX02
{
    internal class Player
    {
        private readonly string r_PlayerName;
        private readonly bool r_IsComputer;
        private int m_PlayerPoints;

        internal Player(string i_PlayerName, bool i_IsComputer)
        {
            r_PlayerName = i_PlayerName;
            r_IsComputer = i_IsComputer;
            m_PlayerPoints = 0;
        }

        internal string PlayerName
        {
            get
            {
                return r_PlayerName;
            }
        }

        internal bool IsComputer
        {
            get
            {
                return r_IsComputer;
            }
        }

        internal int PlayerPoints
        {
            get
            {
                return m_PlayerPoints;
            }

            set
            {
                m_PlayerPoints = value;
            }
        }
    }
}
