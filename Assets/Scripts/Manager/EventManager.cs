using Interact;

namespace Manager
{
    public class EventManager {
        public static EventManager Instance { get { if (_instance == null) { _instance = new EventManager(); } return _instance; } }

        private static EventManager _instance;

        public delegate void DelGrabable(Grabable grabable);

        public event DelGrabable OnGrabableSpawnedEvent;
        public event DelGrabable OnGrabableRemovedEvent;
        public event DelGrabable OnGrabableDestroyedEvent;
        
        public event DelGrabable OnProblemSolvedEvent;

        private void GrabableSpawnedEvent(Grabable grabable) {
            OnGrabableSpawnedEvent?.Invoke(grabable);
        }

        public void GrabableSpawned(Grabable grabable) {
            GrabableSpawnedEvent(grabable);
        }
        
        private void GrabableRemovedEvent(Grabable grabable) {
            OnGrabableRemovedEvent?.Invoke(grabable);
        }

        public void GrabableRemoved(Grabable grabable) {
            GrabableRemovedEvent(grabable);
        }
        
        private void ProblemSolvedEvent(Grabable grabable) {
            OnProblemSolvedEvent?.Invoke(grabable);
        }

        public void ProblemSolved(Grabable grabable) {
            ProblemSolvedEvent(grabable);
        }
        
        private void GrabableDestroyedEvent(Grabable grabable) {
            OnGrabableDestroyedEvent?.Invoke(grabable);
        }

        public void GrabableDestroyed(Grabable grabable) {
            GrabableDestroyedEvent(grabable);
        }
    }
}