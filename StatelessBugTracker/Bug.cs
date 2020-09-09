using Stateless;

namespace StatelessBugTracker
{
    public class Bug
    {
        private State _state = State.Open;
        private StateMachine<State, Trigger> _machine;
        private StateMachine<State, Trigger>.TriggerWithParameters<string> _assignTrigger;
        
        private string _title;
        public string Title => _title;

        private string _assignee;
        public string Assignee => _assignee;

        public string CurrentState => _machine.State.ToString();

        public Bug(string title)
        {
            _title = title;
            _machine = new StateMachine<State, Trigger>(() => _state, s => _state = s);
            _assignTrigger = _machine.SetTriggerParameters<string>(Trigger.Assign);

            _machine.Configure(State.Open)
                .Permit(Trigger.Assign, State.Assigned)
                .Permit(Trigger.Close, State.Closed);

            _machine.Configure(State.Assigned)
                .OnEntryFrom(_assignTrigger, assignee => _assignee = assignee)
                .SubstateOf(State.Open)
                .PermitReentry(Trigger.Assign)
                .Permit(Trigger.Close, State.Closed)
                .Permit(Trigger.Defer, State.Deferred);

            _machine.Configure(State.Deferred)
                .OnEntry(() => _assignee = null)
                .Permit(Trigger.Assign, State.Assigned);
        }

        public void Assign(string assignee)
        {
            _machine.Fire(_assignTrigger, assignee);
        }

        public void Defer()
        {
            _machine.Fire(Trigger.Defer);
        }

        public void Close()
        {
            _machine.Fire(Trigger.Close);
        }
    }
}