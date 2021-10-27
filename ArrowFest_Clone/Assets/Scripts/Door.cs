using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, IBooster
{
    public enum Position
    {
        Right,
        Left
    };


    private Dictionary<IBooster.Operator, string> operatorSigns = new Dictionary<IBooster.Operator, string>
    {
        { IBooster.Operator.Addition, "+" },
        { IBooster.Operator.Subtraction, "-" },
        { IBooster.Operator.Multiplication, "x" },
        { IBooster.Operator.Division, "÷" }
    };

    [SerializeField] private IBooster.Operator _operatorType;
    [SerializeField] private Position _position;
    [SerializeField] private int _value;

    public int Value { get => _value; }
    public IBooster.Operator OperatorType { get => _operatorType; }

    // Start is called before the first frame update
    void Start()
    {
        SetValueText();
        SetColor();
        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetColor()
    {
        var renderer = transform.Find("Glass").GetComponent<MeshRenderer>();
        renderer.material = _operatorType == IBooster.Operator.Addition || _operatorType == IBooster.Operator.Multiplication ? GameManager.Instance.blueMaterial : GameManager.Instance.redMaterial;
    }

    private void SetValueText()
    {
        GetComponentInChildren<Text>().text = operatorSigns[_operatorType] + _value;
    }
    private void SetPosition()
    {
        float offset = _position == Position.Right ? 0.25f : -0.25f;
        transform.position = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
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
                arrows.RemoveArrows((Value - 1) * player.ArrowCount / Value);
                break;
            default:
                break;
        }
    }
}
