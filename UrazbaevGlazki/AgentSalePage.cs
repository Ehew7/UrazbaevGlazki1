using System;

namespace UrazbaevGlazki
{
    internal class AgentSalePage : Uri
    {
        private Agent currentAgent;

        public AgentSalePage(Agent currentAgent)
        {
            this.currentAgent = currentAgent;
        }
    }
}