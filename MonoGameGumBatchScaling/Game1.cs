using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGameGum;
using RenderingLibrary.Graphics;

namespace MonoGameGumBatchScaling;

public class Game1 : Game, IDisposable
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private ViewportAdapter _viewportAdapter;
    private Camera<Vector2> _camera;
    private Texture2D _background;

    private const int WORLD_WIDTH = 640;
    private const int WORLD_HEIGHT = 480;
    private Vector2 WorldCenter = new Vector2(WORLD_WIDTH / 2, WORLD_HEIGHT / 2);
    private GumBatch _gumBatch;
    private BitmapFont _fontBase;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = WORLD_WIDTH;
        _graphics.PreferredBackBufferHeight = WORLD_HEIGHT;
        _graphics.ApplyChanges();

        _viewportAdapter = new BoxingViewportAdapter(
            Window, 
            _graphics.GraphicsDevice, 
            _graphics.PreferredBackBufferWidth, 
            _graphics.PreferredBackBufferHeight
        );

        _camera = new OrthographicCamera(_viewportAdapter);
        _camera.LookAt(WorldCenter);

        GumService.Default.Initialize(this);
        _gumBatch = new GumBatch();

        Window.ClientSizeChanged += OnClientSizeChanged;

        _fontBase = new BitmapFont("font-16x16.fnt");
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _background = Content.Load<Texture2D>("backgrounds/1");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());

        _spriteBatch.Draw(
            _background, 
            WorldCenter,
            null,
            Color.White,
            0f,
            new Vector2(_background.Width / 2, _background.Height / 2),
            Vector2.One,
            SpriteEffects.None,
            0f
        );

        _spriteBatch.End();

        _gumBatch.Begin();

        _gumBatch.DrawString(
            _fontBase,
            "This is using Gum Batch",
            new Vector2(24, 144),
            Color.White
        );

        _gumBatch.End();

        base.Draw(gameTime);
    }

    private void OnClientSizeChanged(object sender, EventArgs e)
    {
        // Updating the canvas width and height when client size changes.
        GumService.Default.CanvasWidth = 
            GraphicsDevice.PresentationParameters.BackBufferWidth;
        GumService.Default.CanvasHeight = 
            GraphicsDevice.PresentationParameters.BackBufferHeight;

    }

    public new void Dispose()
    {
        _viewportAdapter.Dispose();
    }
}
