using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(TapGesture), typeof(TransformGesture))]
public class PlayerMovimentController : MonoBehaviour
{
    private Player _player;
    private Ball _ball;

    //TRANSFORM GESTURE
    private TapGesture _tapGesture;

    private TransformGesture _transformGesture;

    [SerializeField]
    private float _minDistanzaTiro;

    // Start is called before the first frame update
    private void Start()
    {
        _player = GetComponent<Player>();
        _ball = Ball.current;

        _tapGesture = transform.GetComponent<TapGesture>();
        _transformGesture = transform.GetComponent<TransformGesture>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_player.GetState() != Player.EStatoAnimation.Possesso && _ball.GetState() == Ball.EStato.Libera)
        {
            var posBall = _ball.transform.position;
            _player.SetNuotoStile();
            MoveToPosition(posBall);
        }
    }

    private void MoveToPosition(Vector3 pos)
    {
        var dir = transform.position - pos;
        var step = _player.GetSpeed() * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, pos, step);

        var rot = Quaternion.AngleAxis(Utility.GetAngleBetweenPosAB(transform.position, pos), Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            var sp = _player.GetState();

            if (CheckPossesso(sp))
                return;
        }
    }

    private void HandleTransformUpdate(object sender, System.EventArgs e)
    {
        var posScreen = _transformGesture.ScreenPosition;
        var posInWorld = Camera.main.ScreenToWorldPoint(posScreen);

        Utility.RotateObjToPoint(gameObject, posInWorld);
    }

    private void HandleTransformCompleted(object sender, System.EventArgs e)
    {

        var posScreen = _transformGesture.ScreenPosition;
        var posInWorld = Camera.main.ScreenToWorldPoint(posScreen);

        if (Vector2.Distance(transform.position, posInWorld) < _minDistanzaTiro)
            return;

        _ball.SetFinalPos(posInWorld);
        _ball.SetPower(_player.GetShoot());
        _player.SetCaricaTiro();
        DeactiveTransformGesture();
    }

    private void HandleTapGesture(object sender, System.EventArgs e)
    {
    }

    private void ActiveTrasformGesture()
    {
        _tapGesture.Tapped += HandleTapGesture;
        _transformGesture.TransformCompleted += HandleTransformCompleted;
        _transformGesture.Transformed += HandleTransformUpdate;
    }

    private void DeactiveTransformGesture()
    {
        _tapGesture.Tapped -= HandleTapGesture;
        _transformGesture.TransformCompleted -= HandleTransformCompleted;
        _transformGesture.Transformed -= HandleTransformUpdate;
    }

    private bool CheckPossesso(Player.EStatoAnimation state)
    {
        if (_ball.GetState() == Ball.EStato.Libera && state != Player.EStatoAnimation.Possesso && state != Player.EStatoAnimation.CaricaTiro)
        {
            _player.SetPossesso();
            ActiveTrasformGesture();
            _ball.KeepBall(_player._keepBallPosition.transform);
            return true;
        }
        return false;
    }
}