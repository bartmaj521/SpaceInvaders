﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;
using SFML.Graphics;
using System.IO;

namespace SpaceInvaders.Classes.GUI
{
    public abstract class Menu : Scene
    {

        protected List<UIComponent> componentList = new List<UIComponent>();
        protected Cursor cursor;
        protected bool initialized;
        protected Sprite background;

        public Menu()
        {
            background = new Sprite(new Texture(ResourcesManager.resourcesPath + "bg.png"));
            initialized = false;
        }

        public override void callOnKeyPressed(object sender, KeyEventArgs e, SceneManager sceneManager)
        {
            foreach (OurTextbox txb in componentList.OfType<OurTextbox>())
            {
                txb.checkKey(e);
            }
        }
        public override void callOnMouseButtonPressed(object sender, MouseButtonEventArgs e, SceneManager sceneManager)
        {
            foreach (OurButton button in componentList.OfType<OurButton>())
            {
                button.checkClick(new Vector2f(e.X, e.Y), e.Button, sender);
            }
        }
        public override void callOnMouseButtonReleased(object sender, MouseButtonEventArgs e, SceneManager sceneManager)
        {
            foreach (OurButton button in componentList.OfType<OurButton>())
                button.checkUnclick(new Vector2f(e.X, e.Y), e.Button, sender);
        }
        public override void callOnMoved(object sender, MouseMoveEventArgs e, SceneManager sceneManager)
        {
            foreach (OurButton button in componentList.OfType<OurButton>())
                button.checkHover(new Vector2f(e.X, e.Y));
            cursor.moveCursor(new Vector2f(e.X, e.Y));
        }

        public override void drawComponents(SceneManager sceneManager)
        {
            sceneManager.window.Draw(background);
            foreach (var component in componentList)
                sceneManager.window.Draw(component);
            sceneManager.window.Draw(cursor);
        }
        public override void updateComponents(SceneManager sceneManager)
        {
            foreach (var component in componentList)
            {
                component.update();
            }
        }
    }
}