using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMeter : MonoBehaviour
{
    //Apply the dial textures
    [SerializeField] private Texture2D dialTex;
    [SerializeField] private Texture2D needleTex;
    //Apply the dialPosition
    [SerializeField] private RectTransform dialPos;
    //apply the angles of the dial
    [SerializeField] private float stopAngle;
    [SerializeField] private float topSpeedAngle;
    //Apply the topSpeed/currentSpeed
    [SerializeField] private float topSpeed;
    private float speed;
    private PhysicsComponent physcomp;
    [SerializeField] private Font font;
    private float speedTextValue;
    private double pickUpTextValue;
    private Rect rect_Label;
    GUIStyle style = new GUIStyle();


    private void Awake()
    {
        physcomp = GameObject.FindWithTag("Player").GetComponent<PhysicsComponent>();
        style.normal.textColor = Color.cyan;


    }

    private void OnGUI()
    {
        if (physcomp != null)
        {
            Vector3 playerSpeed = new Vector3(physcomp.GetVelocity().x, 0f, physcomp.GetVelocity().z);
            speedTextValue = Mathf.FloorToInt(playerSpeed.magnitude);
            pickUpTextValue = System.Math.Round((physcomp.GetSpeedIncrease() - 1) * 100,2);

           
            playerSpeed *= physcomp.GetSpeedIncrease();
            speed = playerSpeed.magnitude;
            GUI.DrawTexture(new Rect(dialPos.position.x, dialPos.position.y, dialTex.width, dialTex.height), dialTex);
            Vector2 centre = new Vector2(dialPos.position.x + dialTex.width / 2, dialPos.position.y + dialTex.height / 2);
            Matrix4x4 savedMatrix = GUI.matrix;
            float speedFraction = (speed / topSpeed);
            float needleAngle = Mathf.Lerp(stopAngle, topSpeedAngle, speedFraction);
            GUIUtility.RotateAroundPivot(needleAngle, centre);
            GUI.DrawTexture(new Rect(centre.x, centre.y - needleTex.height / 2f, needleTex.width / 1.5f, needleTex.height), needleTex);
            GUI.matrix = savedMatrix;
            GUI.skin.font = font;

            rect_Label = new Rect(dialPos.position.x + 70, dialPos.position.y + 155, 120, 30);

            GUI.Label(rect_Label, "" + speedTextValue, style);

            rect_Label = new Rect(dialPos.position.x + 150, dialPos.position.y + 155, 120, 30);

            GUI.Label(rect_Label, "+" + pickUpTextValue + "%", style);
         
        }
    }
}
