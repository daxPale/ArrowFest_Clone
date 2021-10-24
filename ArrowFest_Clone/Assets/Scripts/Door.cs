using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public enum Position
    {
        Right,
        Left
    };

    public enum Operator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    };

    private Dictionary<Operator, string> operatorSigns = new Dictionary<Operator, string>
    {
        { Operator.Addition, "+" },
        { Operator.Subtraction, "-" },
        { Operator.Multiplication, "x" },
        { Operator.Division, "÷" }
    };

    [SerializeField] private Operator _operatorType;
    [SerializeField] private Position _position;
    [SerializeField] private int _value;

    public int Value { get => _value; }
    public Operator OperatorType { get => _operatorType; }

    private void SetColor()
    {
        var renderer = transform.Find("Glass").GetComponent<MeshRenderer>();
        renderer.material = _operatorType == Operator.Addition || _operatorType == Operator.Multiplication ? GameManager.Instance.blueMaterial : GameManager.Instance.redMaterial;
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

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();

        switch (OperatorType)
        {
            case Operator.Addition:
                player.AddArrows(Value);
                break;
            case Operator.Subtraction:
                player.RemoveArrows(Value);
                break;
            case Operator.Multiplication:                
                player.AddArrows(player.ArrowCount * (Value - 1));
                break;
            case Operator.Division:
                player.RemoveArrows(player.ArrowCount * (1 - Value));
                break;
            default:
                break;
        }
    }
 
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
}
