using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLand : MonoBehaviour, IBooster
{
    [SerializeField] private IBooster.Operator _operatorType;
    [SerializeField] private int _value;

    public int Value { get => _value; }
    public IBooster.Operator OperatorType { get => _operatorType; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Boost(GameObject other)
    {
        var player = other.gameObject.GetComponent<Player>();
        var arrows = other.gameObject.GetComponentInChildren<ArrowSystem>();

        switch (OperatorType)
        {
            case IBooster.Operator.Addition:
                arrows.AddArrows(Value);
                break;
            case IBooster.Operator.Subtraction:
                arrows.RemoveArrows(Value);
                break;
            case IBooster.Operator.Multiplication:
                arrows.AddArrows(player.ArrowCount * (Value - 1));
                break;
            case IBooster.Operator.Division:
                arrows.RemoveArrows(player.ArrowCount * (1 - Value));
                break;
            default:
                break;
        }
    }
}
