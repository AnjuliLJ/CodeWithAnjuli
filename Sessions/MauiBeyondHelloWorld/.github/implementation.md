# Chart Replacement Research for .NET MAUI

## Current State
- Currently using **Syncfusion.Maui.Charts** (version 31.1.22)
- Chart displays column/bar chart with:
  - Category axis (X-axis)
  - Numerical axis (Y-axis)
  - Data labels inside columns
  - Custom styling (blue columns, white text labels)
  - Dynamic data binding to ChartDataPoint collection

## Requirements
- Column/Bar chart support
- Data binding capabilities
- Customizable styling (colors, labels)
- Data labels on columns
- Cross-platform (iOS, Android, macOS Catalyst, Windows)
- Free or open-source preferred

---

## Option 1: Microcharts (Recommended ⭐)

**Package**: `Microcharts.Maui`

### Pros:
✅ **Free and open-source** (MIT license)  
✅ Very lightweight and simple to use  
✅ Good for simple charts (bar, line, donut, pie, radar)  
✅ Cross-platform support  
✅ Active maintenance  
✅ No complex licensing  
✅ Easy to customize colors  
✅ Works well for dashboard/summary charts  

### Cons:
❌ Less feature-rich than commercial solutions  
❌ Limited interactivity  
❌ Fewer customization options for complex scenarios  
❌ Documentation is minimal  

### Implementation Complexity: **Low** ⭐⭐⭐⭐⭐

### Code Example:
```xaml
<microcharts:ChartView Chart="{Binding BarChart}"
                       HeightRequest="300"
                       BackgroundColor="White" />
```

```csharp
// In ViewModel
public ChartEntry[] Entries { get; set; }
public BarChart BarChart { get; set; }

private void LoadChart()
{
    Entries = ChartData.Select(d => new ChartEntry((float)d.Amount)
    {
        Label = d.Label,
        ValueLabel = $"€{d.Amount:F0}",
        Color = SKColor.Parse("#5B9BED")
    }).ToArray();
    
    BarChart = new BarChart { Entries = Entries };
}
```

### NuGet Package:
```
dotnet add package Microcharts.Maui
```

---

## Option 2: LiveChartsCore (Advanced Alternative)

**Package**: `LiveChartsCore.SkiaSharpView.Maui`

### Pros:
✅ **Free and open-source** (MIT license)  
✅ Very powerful and feature-rich  
✅ Excellent documentation  
✅ Highly customizable  
✅ Smooth animations  
✅ Interactive charts  
✅ Multiple chart types  
✅ Active development  

### Cons:
❌ More complex API (steeper learning curve)  
❌ Heavier dependency (SkiaSharp)  
❌ May be overkill for simple charts  

### Implementation Complexity: **Medium** ⭐⭐⭐

### Code Example:
```xaml
<lvc:CartesianChart Series="{Binding Series}"
                    XAxes="{Binding XAxes}"
                    YAxes="{Binding YAxes}"
                    HeightRequest="300" />
```

```csharp
// In ViewModel
public ISeries[] Series { get; set; }

private void LoadChart()
{
    Series = new ISeries[]
    {
        new ColumnSeries<ChartDataPoint>
        {
            Values = ChartData,
            Mapping = (dataPoint, point) =>
            {
                point.PrimaryValue = (double)dataPoint.Amount;
                point.SecondaryValue = point.Context.Index;
            },
            Fill = new SolidColorPaint(SKColors.Parse("#5B9BED")),
            DataLabelsSize = 11,
            DataLabelsPaint = new SolidColorPaint(SKColors.White)
        }
    };
}
```

### NuGet Package:
```
dotnet add package LiveChartsCore.SkiaSharpView.Maui
```

---

## Option 3: OxyPlot (Mature Option)

**Package**: `OxyPlot.Maui`

### Pros:
✅ **Free and open-source** (MIT license)  
✅ Very mature library (existed for years)  
✅ Good documentation  
✅ Wide variety of chart types  
✅ Cross-platform  

### Cons:
❌ .NET MAUI support is newer/less mature  
❌ Less modern API  
❌ Fewer built-in animations  
❌ Styling can be verbose  

### Implementation Complexity: **Medium** ⭐⭐⭐

---

## Option 4: Custom SkiaSharp Implementation

**Package**: `SkiaSharp.Views.Maui.Controls`

### Pros:
✅ Full control over rendering  
✅ SkiaSharp already commonly used in MAUI  
✅ Can create exactly what you need  
✅ Best performance  

### Cons:
❌ **High development time**  
❌ Need to implement everything from scratch  
❌ Maintenance burden  
❌ Not recommended unless very specific needs  

### Implementation Complexity: **Very High** ⭐

---

## Option 5: DrawableView (MAUI Native)

**Built-in**: Uses `Microsoft.Maui.Graphics`

### Pros:
✅ No external dependencies  
✅ Built into MAUI  
✅ Lightweight  
✅ Full control  

### Cons:
❌ Need to implement chart logic yourself  
❌ No built-in chart types  
❌ Time-consuming  
❌ Reinventing the wheel  

### Implementation Complexity: **Very High** ⭐

---

## Recommendation Summary

### 🏆 Best Choice: **Microcharts.Maui**

**Why?**
1. **Simplicity**: Easiest to implement and maintain
2. **Free**: No licensing concerns
3. **Sufficient**: Meets all current requirements for FinanceBuddy
4. **Lightweight**: Won't bloat the app
5. **Clean API**: Minimal code changes needed
6. **Perfect for dashboards**: Exactly the use case in this app

### Migration Effort Estimate:
- **Low complexity**: ~1-2 hours
- Remove Syncfusion package
- Install Microcharts.Maui
- Update ChartsPage.xaml (replace chart control)
- Update ChartsViewModel (convert ChartData to ChartEntry[])
- Test on all platforms

### 🥈 Alternative: **LiveChartsCore** (if you need more features later)

**When to use?**
- Need advanced interactivity (tooltips, zooming, etc.)
- Need animations
- Plan to add more complex charts in the future
- Want a more professional, polished look

---

## Implementation Plan for Microcharts

### Step 1: Remove Syncfusion
```bash
dotnet remove package Syncfusion.Maui.Charts
```

Remove from MauiProgram.cs:
- `.ConfigureSyncfusionCore()`
- License registration line

### Step 2: Add Microcharts
```bash
dotnet add package Microcharts.Maui
```

### Step 3: Update ChartsViewModel
- Convert `ObservableCollection<ChartDataPoint>` to `ChartEntry[]`
- Create `BarChart` property
- Update `LoadChartData()` to create entries

### Step 4: Update ChartsPage.xaml
- Replace `chart:SfCartesianChart` with `microcharts:ChartView`
- Bind to `BarChart` property
- Simplify XAML significantly

### Step 5: Test
- Verify chart displays correctly
- Check period switching (Week/Month/Year)
- Verify category filtering
- Test on multiple platforms

---

## Code Comparison

### Before (Syncfusion - Complex):
```xaml
<chart:SfCartesianChart>
    <chart:SfCartesianChart.XAxes>
        <chart:CategoryAxis ShowMajorGridLines="False" />
    </chart:SfCartesianChart.XAxes>
    <chart:SfCartesianChart.YAxes>
        <chart:NumericalAxis ShowMajorGridLines="True" />
    </chart:SfCartesianChart.YAxes>
    <chart:ColumnSeries ItemsSource="{Binding ChartData}"
                        XBindingPath="Label"
                        YBindingPath="Amount"
                        Fill="#5B9BED"
                        ShowDataLabels="True">
        <!-- Complex data label configuration -->
    </chart:ColumnSeries>
</chart:SfCartesianChart>
```

### After (Microcharts - Simple):
```xaml
<microcharts:ChartView Chart="{Binding BarChart}"
                       HeightRequest="300"
                       BackgroundColor="White" />
```

---

## Conclusion

**Microcharts.Maui** is the best replacement for this project because:
- ✅ Removes licensing dependency
- ✅ Simpler codebase
- ✅ Faster build times (smaller dependency)
- ✅ Easier to maintain
- ✅ Perfect for the dashboard use case
- ✅ Free forever

The app's charts are primarily for **data visualization** rather than **complex data analysis**, making Microcharts an ideal fit.
