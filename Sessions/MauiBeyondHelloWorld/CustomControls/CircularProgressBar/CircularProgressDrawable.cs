using System;

namespace CustomControls.CircularProgressBar;

public class CircularProgressDrawable: IDrawable
{
	public int Progress { get; set; }
	public int Size { get; set; }
	public int Thickness { get; set; }
	public Color ProgressColor { get; set; } = Colors.Blue;

	public void Draw(ICanvas canvas, RectF dirtyRect)
	{
		if (Size <= 0 || Thickness <= 0) return;

		float effectiveSize = Size - Thickness;
		float x = Thickness / 2f;
		float y = Thickness / 2f;

		int clampedProgress = Math.Max(0, Math.Min(100, Progress));

		if (clampedProgress > 0)
		{
			float angle = GetAngle(clampedProgress);

			// Draw progress arc
			canvas.StrokeColor = ProgressColor;
			canvas.StrokeSize = Thickness;
			canvas.DrawArc(x, y, effectiveSize, effectiveSize, 90, angle, true, false);
		}
	}

	private float GetAngle(int progress)
	{
		float factor = 90f / 25f;

		if (progress > 75)
		{
			return -180 - ((progress - 75) * factor);
		}
		else if (progress > 50)
		{
			return -90 - ((progress - 50) * factor);
		}
		else if (progress > 25)
		{
			return 0 - ((progress - 25) * factor);
		}
		else
		{
			return 90 - (progress * factor);
		}
	}
}
