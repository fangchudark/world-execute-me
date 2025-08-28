using Godot;
using System;
using System.Collections.Generic;

[Tool]
[GlobalClass]
public partial class MouseAvoidText : Control
{
    [ExportGroup("Label Settings")]

    [Export(PropertyHint.MultilineText)]
    public string Text { get; set; } // 显示的文本内容
    [Export(PropertyHint.LocaleId)]
    public string Language { get; set; } = "en"; // 文本语言设置
    [Export] public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Center; // 水平对齐方式
    [Export] public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Center; // 垂直对齐方式    


    [ExportGroup("Font Settings")]
    [Export]
    public Font Font
    {
        get => _font;
        set
        {
            if (value == null)            
                _font = GetThemeFont("font");
            else
                _font = value;

            _fontRid = _font.GetRids();
        }
    }
    Font _font;
    
    [Export(PropertyHint.Range, "1,4096")]
    public int FontSize { get; set; } = 16;
    [Export] public Color FontColor { get; set; } = Colors.White;

    [ExportGroup("Avoidance Settings")]
    [Export] public float AvoidRadius { get; set; } = 1000f; // 鼠标避让半径
    [Export] public float MaxOffset { get; set; } = 100f;    // 最大避让偏移量
    [Export] public float SmoothFactor { get; set; } = 10f;  // 避让动画的平滑系数

    // 私有变量
    Vector2 _mousePosition;          // 存储当前鼠标位置
    Vector2[] _originalOffsets;     // 存储字符原始偏移量
    Vector2[] _currentOffsets;      // 存储字符当前偏移量
    readonly List<Vector2> _glyphPositions = []; // 存储所有字符的位置
    readonly TextServer _textServer = TextServerManager.GetPrimaryInterface();
    Rid _shapedBuffer;
    Godot.Collections.Array<Rid> _fontRid;

    // 节点准备就绪时调用
    public override void _Ready()
    {
        // 设置纹理过滤模式为最近邻(保持像素清晰)
        TextureFilter = TextureFilterEnum.Nearest;
        Font ??= GetThemeFont("font");
    }

    // 每帧调用的处理函数
    public override void _Process(double delta)
    {
        // 获取当前鼠标的全局位置
        _mousePosition = GetGlobalMousePosition();
        // 更新文本偏移量
        UpdateTextOffsets();
    }

    // 更新文本字符偏移量的核心方法
    private void UpdateTextOffsets()
    {
        // 如果文本为空则直接返回
        if (string.IsNullOrEmpty(Text) || Font == null)
            return;
        // 创建文本缓冲区
        _shapedBuffer = _textServer.CreateShapedText();

        // 向缓冲区添加文本字符串
        _textServer.ShapedTextAddString(
            _shapedBuffer,
            Text,
            _fontRid,
            FontSize,
            opentypeFeatures: null,
            language: Language,
            meta: default
        );

        // 获取所有字符信息
        var glyphs = _textServer.ShapedTextGetGlyphs(_shapedBuffer);

        AlignGlyphPositions(glyphs);

        // 初始化偏移数组(第一次或字符数量变化时)
        if (_originalOffsets == null || _originalOffsets.Length != _glyphPositions.Count)
        {
            _originalOffsets = new Vector2[_glyphPositions.Count];
            _currentOffsets = new Vector2[_glyphPositions.Count];
            Array.Fill(_originalOffsets, Vector2.Zero);
            Array.Fill(_currentOffsets, Vector2.Zero);
        }

        // 计算每个字符的目标偏移量(避让效果)
        for (int i = 0; i < _glyphPositions.Count; i++)
        {
            // 计算字符的全局位置
            var glyphGlobalPos = GlobalPosition + _glyphPositions[i];
            // 计算字符到鼠标的向量
            var toMouse = glyphGlobalPos - _mousePosition;
            // 计算距离
            var distance = toMouse.Length();

            // 如果鼠标在避让半径内
            if (distance < AvoidRadius)
            {
                // 计算避让方向和强度
                var direction = toMouse.Normalized();
                var strength = 1 - (distance / AvoidRadius);
                strength = Mathf.Clamp(strength, 0, 1);

                // 计算目标偏移量(方向*最大偏移*强度)
                var targetOffset = direction * MaxOffset * strength;

                // 平滑过渡到目标偏移
                _currentOffsets[i] = _currentOffsets[i].Lerp(targetOffset, (float)GetProcessDeltaTime() * SmoothFactor);
            }
            else
            {
                // 平滑回到原始位置(无偏移)
                _currentOffsets[i] = _currentOffsets[i].Lerp(Vector2.Zero, (float)GetProcessDeltaTime() * SmoothFactor);
            }
        }

        // 请求重绘
        QueueRedraw();
    }
    
    // 计算文本位置，调整排版
    private void AlignGlyphPositions(Godot.Collections.Array<Godot.Collections.Dictionary> glyphs)
    {
        // 遍历每个字符计算位置
        // 先遍历一次，拆分所有行，记录每行的glyph索引范围和宽度
        var lines = new List<(int start, int end, float width, float ascent, float descent)>();
        int lineStart = 0;
        float lineWidth = 0f, maxAscent = 0f, maxDescent = 0f;
        for (int i = 0; i < glyphs.Count; i++)
        {
            var glyph = glyphs[i];
            var advance = (float)glyph["advance"];
            var index = (int)glyph["index"];
            float ascent = Font.GetAscent(FontSize);
            float descent = Font.GetDescent(FontSize);
            if (ascent > maxAscent) maxAscent = ascent;
            if (descent > maxDescent) maxDescent = descent;

            // 检查换行
            if (advance == 0 && index == 0 && i != 0)
            {
                lines.Add((lineStart, i, lineWidth, maxAscent, maxDescent));
                lineStart = i + 1;
                lineWidth = 0f;
                maxAscent = 0f;
                maxDescent = 0f;
                continue;
            }
            lineWidth += advance;
        }
        if (lineStart < glyphs.Count)
            lines.Add((lineStart, glyphs.Count, lineWidth, maxAscent, maxDescent));

        // 计算总文本高度
        float totalTextHeight = 0f;
        foreach (var (start, end, width, ascent, descent) in lines)
            totalTextHeight += ascent + descent;


        // 逐行排版
        // 计算垂直分布（Fill模式）
        _glyphPositions.Clear();

        float[] lineYs = new float[lines.Count];
        if (VerticalAlignment == VerticalAlignment.Fill && lines.Count > 1)
        {
            float totalLineHeight = 0f;
            foreach (var (start, end, width, ascent, descent) in lines)
                totalLineHeight += ascent + descent;
            float lineSpacing = (Size.Y - totalLineHeight) / (lines.Count - 1);
            float curY = 0f;
            for (int l = 0; l < lines.Count; l++)
            {
                lineYs[l] = curY;
                curY += lines[l].ascent + lines[l].descent + lineSpacing;
            }
        }
        else
        {
            float startY = VerticalAlignment switch
            {
                VerticalAlignment.Top => 0f,
                VerticalAlignment.Center => (Size.Y - totalTextHeight) / 2f,
                VerticalAlignment.Bottom => Size.Y - totalTextHeight,
                _ => 0f,
            };
            float curY = startY;
            for (int l = 0; l < lines.Count; l++)
            {
                lineYs[l] = curY;
                curY += lines[l].ascent + lines[l].descent;
            }
        }

        // 每行处理
        for (int lineIdx = 0; lineIdx < lines.Count; lineIdx++)
        {

            var (start, end, width, ascent, _) = lines[lineIdx];
            float startX;
            float[] charXs = new float[end - start];

            // 水平 Fill
            if (HorizontalAlignment == HorizontalAlignment.Fill && (end - start) > 1)
            {
                // 计算所有字符宽度
                float totalCharWidth = 0f;
                for (int i = start; i < end; i++)
                {
                    var glyph = glyphs[i];
                    totalCharWidth += (float)glyph["advance"];
                }
                float charSpacing = (Size.X - totalCharWidth) / (end - start - 1);
                float x = 0f;
                for (int i = start, c = 0; i < end; i++, c++)
                {
                    charXs[c] = x;
                    x += (float)glyphs[i]["advance"] + charSpacing;
                }

            }
            else
            {
                // 普通对齐
                startX = HorizontalAlignment switch
                {
                    HorizontalAlignment.Left => 0f,
                    HorizontalAlignment.Center => (Size.X - width) / 2f,
                    HorizontalAlignment.Right => Size.X - width,
                    _ => 0f,
                };
                float x = startX;
                for (int i = start, c = 0; i < end; i++, c++)
                {
                    charXs[c] = x;
                    x += (float)glyphs[i]["advance"];
                }
            }

            // 填充_glyphPositions
            for (int i = start, c = 0; i < end; i++, c++)
            {
                var glyph = glyphs[i];
                var offset = (Vector2)glyph["offset"];
                _glyphPositions.Add(new Vector2(charXs[c], lineYs[lineIdx] + ascent) + offset);
            }
        }
    }
    
    // 自定义绘制方法
    public override void _Draw()
    {
        // 如果文本为空或没有字符位置数据则直接返回
        if (string.IsNullOrEmpty(Text) || _glyphPositions.Count == 0)
            return;

        if (Font == null || !_shapedBuffer.IsValid)
            return;

        // 绘制每个字符，应用计算好的偏移量
        var glyphs = _textServer.ShapedTextGetGlyphs(_shapedBuffer);
        int glyphPosIndex = 0; // 字符位置索引

        for (int i = 0; i < glyphs.Count; i++)
        {
            var glyph = glyphs[i];
            var fontRid = (Rid)glyph["font_rid"]; // 字体资源ID
            var index = (int)glyph["index"];      // 字符索引
            var offset = (Vector2)glyph["offset"]; // 字符偏移量
            var advance = (float)glyph["advance"]; // 字符前进量

            // 跳过换行符(与UpdateTextOffsets保持一致)
            if (advance == 0 && index == 0 && i != 0)
                continue;

            // 计算最终字符位置(基础位置+避让偏移)
            var glyphPos = _glyphPositions[glyphPosIndex] + _currentOffsets[glyphPosIndex];

            // 如果字体有效则绘制字符
            if (fontRid.IsValid)
            {
                _textServer.FontDrawGlyph(
                    fontRid,
                    GetCanvasItem(),
                    FontSize,
                    glyphPos + offset, // 应用字符自身的偏移量
                    index,
                    FontColor
                );
            }
            glyphPosIndex++;
        }
    }
}