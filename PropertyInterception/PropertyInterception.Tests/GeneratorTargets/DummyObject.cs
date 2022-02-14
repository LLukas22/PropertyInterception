using PropertyInterception.Tests.Attributes;

namespace PropertyInterception.Tests.GeneratorTargets
{
    public partial class DummyObject
    {

        public bool TriggeredOnExit;
        public bool TriggeredException;

        [Normal]
        private string normalProperty;
        [Get]
        private string getProperty;
        [Set]
        private string setProperty;
        [Exit]
        private string onExitProperty;
        [Exception]
        private string onExceptionProperty;
    }
}
