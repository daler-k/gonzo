using System;
using UnityEngine;

public class ResolutionAdapter : MonoBehaviour
{
    private static Resolution _resolution;
    public RResolutionAdapt resolution9x16 = new RResolutionAdapt(Resolution.Screen9x16);
    public RResolutionAdapt resolution10x16 = new RResolutionAdapt(Resolution.Screen10x16);
    public RResolutionAdapt resolution11x16 = new RResolutionAdapt(Resolution.Screen11x16);
    public RResolutionAdapt resolution2x3 = new RResolutionAdapt(Resolution.Screen2x3);
    public RResolutionAdapt resolution3x4 = new RResolutionAdapt(Resolution.Screen3x4);
	public RResolutionAdapt resolution600x1024 = new RResolutionAdapt(Resolution.Screen600x1024);
    public RResolutionAdapt resolution3x5 = new RResolutionAdapt(Resolution.Screen3x5);

	private void OnEnable()
    {
        RResolutionAdapt adapt = null;
        switch (resolution)
        {
            case Resolution.Screen3x5:
                adapt = this.resolution3x5;
                break;

            case Resolution.Screen3x4:
                adapt = this.resolution3x4;
                break;

            case Resolution.Screen2x3:
                adapt = this.resolution2x3;
                break;

            case Resolution.Screen9x16:
                adapt = this.resolution9x16;
                break;

            case Resolution.Screen10x16:
                adapt = this.resolution10x16;
                break;

            case Resolution.Screen11x16:
                adapt = this.resolution11x16;
                break;
			
			case Resolution.Screen600x1024:
                adapt = this.resolution600x1024;
                break;
        }
        if ((adapt != null) && adapt.active)
        {
            if (!adapt.enabled)
            {
                base.gameObject.SetActiveRecursively(false);
            }
            Vector3 localPosition = base.transform.localPosition;
            localPosition.x = adapt.position.x;
            localPosition.y = adapt.position.y;
            base.transform.localPosition = localPosition;
            if (adapt.scale.x != 0f)
            {
                base.transform.localScale = new Vector3(adapt.scale.x, adapt.scale.y, 1f);
            }
        }
    }

    public static Resolution resolution
    {
        get
        {
            if (_resolution == Resolution.None)
            {
                float num = ((float) Mathf.Max(Screen.width, Screen.height)) / ((float) Mathf.Min(Screen.width, Screen.height));
                if (num <= 1.343334f)
                {
                    _resolution = Resolution.Screen3x4;
                }
                else if (num <= 1.464546f)
                {
                    _resolution = Resolution.Screen11x16;
                }
                else if (num <= 1.51f)
                {
                    _resolution = Resolution.Screen2x3;
                }
                else if (num <= 1.61f)
                {
                    _resolution = Resolution.Screen10x16;
                }
                else if (num <= 1.676667f)
                {
                    _resolution = Resolution.Screen3x5;
                }
				else if(num <= 1.72)
				{
					_resolution = Resolution.Screen600x1024;
				}
                else if (num <= 1.787778f)
                {
                    _resolution = Resolution.Screen9x16;
                }
                else
                {
                    _resolution = Resolution.Other;
                }
            }
            return _resolution;
        }
    }

    public enum Resolution
    {
        None,
        Screen3x4,
		Screen11x16,
        Screen2x3,
		Screen10x16,
		Screen3x5,
		Screen600x1024,
        Screen9x16,
        Other
    }

    [Serializable]
	
    public class RResolutionAdapt
    {
        public bool active = false;
        public bool enabled;
        public ResolutionAdapter.Resolution resolution;
        public Vector2 position;
        public Vector2 scale;

        public RResolutionAdapt(ResolutionAdapter.Resolution res)
        {
            this.resolution = res;
        }
    }
}

