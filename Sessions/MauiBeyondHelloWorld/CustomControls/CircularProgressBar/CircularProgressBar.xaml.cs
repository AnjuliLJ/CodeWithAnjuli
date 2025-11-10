using System.ComponentModel;

namespace CustomControls.CircularProgressBar;

public partial class CircularProgressBar : ContentView
{
    private readonly CircularProgressDrawable _drawable;	
    public CircularProgressBar()
	{
        InitializeComponent();
        _drawable = new CircularProgressDrawable();
		progressGraphics.Drawable = _drawable;
        PropertyChanged += OnPropertyChanged;
		UpdateDrawable();
	}
	 public static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(int), typeof(CircularProgressBar));
    public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(int), typeof(CircularProgressBar));
    public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(int), typeof(CircularProgressBar));
    public static readonly BindableProperty ProgressColorProperty = BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(CircularProgressBar));
    public static readonly BindableProperty ProgressLeftColorProperty = BindableProperty.Create(nameof(ProgressLeftColor), typeof(Color), typeof(CircularProgressBar));
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CircularProgressBar));

    public int Progress
    {
        get => (int)GetValue(ProgressProperty);
        set => SetValue(ProgressProperty, value);
    }

    public int Size
    {
        get => (int)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public int Thickness
    {
        get => (int)GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }

    public Color ProgressColor
    {
        get => (Color)GetValue(ProgressColorProperty);
        set => SetValue(ProgressColorProperty, value);
    }

    public Color ProgressLeftColor
    {
        get => (Color)GetValue(ProgressLeftColorProperty);
        set => SetValue(ProgressLeftColorProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

	private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		UpdateDrawable();
	}

	private void UpdateDrawable()
	{
		if (_drawable != null)
		{
			_drawable.Progress = Progress;
			_drawable.Size = Size;
			_drawable.Thickness = Thickness;
			_drawable.ProgressColor = ProgressColor;
			progressGraphics.Invalidate();
		}
	}
}