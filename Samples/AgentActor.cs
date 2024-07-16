using System.Collections;
using SBaier.DI;
using TMPro;
using UnityEngine;

namespace SBaier.AI.Samples
{
    public class AgentActor : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private Agent _agent;

        [SerializeField] 
        private TextMeshProUGUI _text;

        [SerializeField] 
        [Range(0.2f, 5f)]
        private float _actionDuration;

        private ReadonlyObservable<string> _actionLog;
        private MutableLog _log;
        private int _actionsAmount = 0;
        
        public void Inject(Resolver resolver)
        {
            _actionLog = resolver.Resolve<ReadonlyObservable<string>>();
            _log = resolver.Resolve<MutableLog>();
        }
        
        public void Initialize()
        {
            _actionLog.OnValueChanged += OnLogChanged;
            StartCoroutine(Act());
        }

        public void Clean()
        {
            _actionLog.OnValueChanged -= OnLogChanged;
            StopAllCoroutines();
        }

        private IEnumerator Act()
        {
            _actionsAmount++;
            _log.SetHeader($"<b>{_actionsAmount}. Guard actions log</b>");
            _agent.Act();
            yield return new WaitForSeconds(_actionDuration);
            StartCoroutine(Act());
        }

        private void OnLogChanged(string formervalue, string newvalue)
        {
            _text.text = newvalue;
        }
    }
}