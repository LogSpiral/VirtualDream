using LogSpiralLibrary;
using LogSpiralLibrary.CodeLibrary.DataStructures;
using LogSpiralLibrary.CodeLibrary.DataStructures.Drawing.ComplexPanel;
using LogSpiralLibrary.CodeLibrary.Utilties;
using LogSpiralLibrary.CodeLibrary.Utilties.BaseClasses;
using LogSpiralLibrary.CodeLibrary.Utilties.Extensions;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;
using Terraria.UI;
using Terraria.UI.Chat;
using VirtualDream.Contents.StarBound.Weapons;

namespace VirtualDream.Contents.StarBound.TimeBackTracking
{
    public class WindOfTime : ModItem
    {
        public override string Texture => base.Texture;//"Terraria/Images/Item_" + ItemID.PlatinumWatch
        public int chargeCount;
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var tip = new TooltipLine(Mod, "Charge", $"目前已经充能了{chargeCount} / 8");
            tip.OverrideColor = Main.hslToRgb(0.75f, 0.75f + MathF.Sin((float)VirtualDreamSystem.ModTime / 120f * MathHelper.Pi) * .25f, .5f);
            tooltips.Add(tip);
        }
        public override void SetDefaults()
        {
            Item.width = Item.height = 36;
            Item.value = -1;
            Item.useTime = Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.holdStyle = ItemHoldStyleID.HoldGolfClub;
            Item.accessory = true;
            Item.UseSound = new SoundStyle("VirtualDream/Assets/Sound/shotgun_reload_clip3");
        }
        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation == 1)
            {
                chargeCount = 0;
                if (SubworldLibrary.SubworldSystem.Current is TimeBackTrackingWorld world)
                {
                    world.timeLeft += 129600;
                }
                else
                {
                    SubworldLibrary.SubworldSystem.Enter<TimeBackTrackingWorld>();
                }
            }
            //Main.NewText("!!");
            return base.UseItem(player);
        }
        public override bool CanUseItem(Player player)
        {
            return chargeCount == 8 || player.itemAnimation > 0;//
        }
        public override void UpdateInventory(Player player)
        {
            //chargeCount = 8;
            //Item.width = Item.height = 16;
            //Item.holdStyle = ItemHoldStyleID.HoldGolfClub;
            //TextureAssets.Item[Type] = ModContent.Request<Texture2D>(Texture);
            if ((int)Main.time == 0 && Main.dayTime && chargeCount < 8)
            {
                chargeCount++;

            }
        }
        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation += new Vector2(-6 * player.direction, -4);
            player.itemRotation += -MathHelper.Pi / 6 * player.direction;
            base.HoldStyle(player, heldItemFrame);
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if ((int)Main.time == 0 && Main.dayTime && chargeCount < 8)
            {
                chargeCount++;
            }

        }
        public override void LoadData(TagCompound tag)
        {
            chargeCount = tag.GetInt("chargeCount");
            base.LoadData(tag);
        }
        public override void SaveData(TagCompound tag)
        {
            tag.Add("chargeCount", chargeCount);
            base.SaveData(tag);
        }
    }
    public class WindOfTimeStand : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<WindOfTimeStandTile>());
            Item.width = 30;
            Item.height = 12;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Purple;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
        }
    }
    public class WeaponRepairSystem : ModSystem
    {
        public UserInterface weaponRepairInterface;
        public WeaponRepairUI repairUI;
        public static WeaponRepairSystem Instance;
        public Dictionary<int, WeaponRepairRecipe> recipes = [];
        public override void Load()
        {
            Instance = this;
            repairUI = new WeaponRepairUI();
            repairUI.Activate();
            weaponRepairInterface = new UserInterface();
            weaponRepairInterface.SetState(repairUI);
            base.Load();
        }
        public override void Unload()
        {
            Instance = null;
            repairUI = null;
            weaponRepairInterface = null;
            base.Unload();
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (WeaponRepairUI.Visible || WeaponRepairUI.timer > 0)

                weaponRepairInterface?.Update(gameTime);
            base.UpdateUI(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            //寻找一个名字为Vanilla: Mouse Text的绘制层，也就是绘制鼠标字体的那一层，并且返回那一层的索引
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            //寻找到索引时
            if (MouseTextIndex != -1)
            {
                //往绘制层集合插入一个成员，第一个参数是插入的地方的索引，第二个参数是绘制层
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                   //这里是绘制层的名字
                   "VirtualDream : WeaponRepairUI",
                   //这里是匿名方法
                   delegate
                   {
                       //当Visible开启时（当UI开启时）
                       if (WeaponRepairUI.timer > 0)
                           //绘制UI（运行exampleUI的Draw方法）
                           repairUI.Draw(Main.spriteBatch);
                       return true;
                   },
                   //这里是绘制层的类型
                   InterfaceScaleType.UI)
               );
            }
            base.ModifyInterfaceLayers(layers);
        }
    }
    public class WeaponRepairRecipe
    {
        public List<Item> ingredients = [];
        public StarboundWeaponBase MainWeapon;
        public int ResultType = -1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weaponBase">主武器</param>
        /// <param name="_ingredients">其它材料，只关注类型和数量</param>
        public WeaponRepairRecipe(StarboundWeaponBase weaponBase, List<Item> _ingredients)
        {
            MainWeapon = weaponBase;
            ingredients = _ingredients;
        }
        public WeaponRepairRecipe(StarboundWeaponBase weaponBase)
        {
            MainWeapon = weaponBase;
        }
        public WeaponRepairRecipe(int resultType)
        {
            ResultType = resultType;
        }
        public WeaponRepairRecipe AddIngredient(Item item)
        {
            ingredients.Add(item);
            return this;
        }
        public WeaponRepairRecipe AddIngredient(int type, int stack = 1) => AddIngredient(new Item(type, stack));
        public WeaponRepairRecipe AddIngredient<T>(int stack = 1) where T : ModItem => AddIngredient(ModContent.ItemType<T>(), stack);
        public WeaponRepairRecipe AddResult(int resultType)
        {
            ResultType = resultType;
            return this;
        }
        public WeaponRepairRecipe SetResult<T>() where T : StarboundWeaponBase
        {
            ResultType = ModContent.ItemType<T>();
            return this;
        }
        public void Register()
        {
            if (ResultType == -1)
                return;
            if (MainWeapon == null)
                return;
            WeaponRepairSystem.Instance.recipes.Add(MainWeapon.Type, this);
        }
    }
    public class WeaponRepairUI : UIState
    {
        public static bool Visible { get; private set; }
        public static int timer;
        /// <summary>
        /// 缩放修复（这公式自己测的，没有游戏依据）
        /// 将屏幕坐标转换为UI坐标
        /// </summary>
        public static Vector2 TransformToUIPosition(Vector2 vector)
        {
            // 获取相对屏幕中心的向量(一定要在调节xy前获取)
            float oppositeX = (vector.X - Main.screenWidth / 2) / Main.UIScale;
            float oppositeY = (vector.Y - Main.screenHeight / 2) / Main.UIScale;
            vector.X = (int)(vector.X / Main.UIScale) + (int)(oppositeX * (Main.GameZoomTarget - 1f));
            vector.Y = (int)(vector.Y / Main.UIScale) + (int)(oppositeY * (Main.GameZoomTarget - 1f));
            return new(vector.X, vector.Y);
        }
        public WindOfTimeStandData standData;
        public static Vector2 MouseScreenUI => TransformToUIPosition(Main.MouseScreen);
        public void Open(WindOfTimeStandData data)
        {
            Visible = true;
            SoundEngine.PlaySound(SoundID.MenuOpen);
            //var Offset = BasePanel.Offset = MouseScreenUI;
            //var Offset = BasePanel.Offset = TransformToUIPosition(Main.ScreenSize.ToVector2() * .5f);
            standData = data;
            if (BasePanel.windOfTimeSlot != null)
            {
                BasePanel.windOfTimeSlot._item = data.item;
            }
            BasePanel.SetUpElementList();

            //var Offset = BasePanel.Offset = TransformToUIPosition(Main.MouseScreen + new Vector2(0, -48));
            var Offset = BasePanel.Offset = TransformToUIPosition(data.tilePosition.ToWorldCoordinates() - Main.screenPosition + new Vector2(0, -64) + new Vector2(24, 0));
            BasePanel.Left.Set(Offset.X - 420, 0f);
            BasePanel.Top.Set(Offset.Y - 128, 0f);
            BasePanel.Recalculate();
        }
        public void Close()
        {
            Visible = false;
            Main.blockInput = false;
            SoundEngine.PlaySound(SoundID.MenuClose);
            if (BasePanel.windOfTimeSlot != null)
            {
                standData.item = BasePanel.windOfTimeSlot._item;
            }
            if (BasePanel.button != null)
                BasePanel.button.recipe = null;
        }
        public bool CacheSetupElements; // 缓存，在下一帧Setup
        public WeaponRepairPanel BasePanel;
        public override void Update(GameTime gameTime)
        {
            if (CacheSetupElements)
            {
                BasePanel.SetUpElementList();
                CacheSetupElements = false;
            }
            timer += Visible ? 1 : -1;
            timer = (int)MathHelper.Clamp(timer, 0, 45);
            BasePanel.factor = MathHelper.SmoothStep(0, 1, timer / 15f);
            var _factor = 0f;
            if (timer > 30)
            {
                _factor = MathHelper.SmoothStep(1, 2, (timer - 30) / 15f);
            }
            else if (timer > 15)
            {
                _factor = MathHelper.SmoothStep(0, 1, (timer - 15) / 15f);
            }
            BasePanel.windOfTimeSlot.factor = _factor;
            foreach (var recipe in BasePanel.recipes)
            {
                recipe.factor = _factor;
            }
            if (BasePanel.button != null)
            {
                BasePanel.button.factor = _factor;
            }
            base.Update(gameTime);
        }
        public override void OnInitialize()
        {
            #region 贴图加载

            #endregion

            #region 面板初始化
            BasePanel = new WeaponRepairPanel()
            {
                Top = StyleDimension.FromPixels(256f),
                HAlign = 0.2f
            };
            //BasePanel.SetPos(new Vector2(256, 256));
            BasePanel.SetSize(512, 256).SetPadding(12f);
            BasePanel.Draggable = true;
            Append(BasePanel);
            #endregion
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            int oldType = BasePanel.windOfTimeSlot._item.type;
            base.Draw(spriteBatch);
            int newType = BasePanel.windOfTimeSlot._item.type;
            if (oldType != newType)
            {
                if (Main.gamePaused)
                {
                    if (BasePanel.windOfTimeSlot != null)
                    {
                        standData.item = BasePanel.windOfTimeSlot._item;
                    }
                }
                else
                    Close();

            }
        }
    }
    public class WeaponRepairPanel : UIElement
    {
        public WindOfTimeRecipeIcon currentRecipe;
        /// <summary>
        /// 是否可拖动
        /// </summary>
        public bool Draggable;
        public bool Dragging;
        public Vector2 Offset;
        public float border;
        public bool CalculateBorder;
        /// <summary>
        /// <br>动画插值</br>
        /// <br>决定ui的大小和透明度之类</br>
        /// </summary>
        public float factor;
        public WeaponRepairPanel(float border = 3, bool CalculateBorder = true)
        {
            SetPadding(10f);
            this.border = border;
            this.CalculateBorder = CalculateBorder;
            OnLeftMouseDown += DragStart;
            OnLeftMouseUp += DragEnd;
        }
        public override void OnActivate()
        {
            base.OnActivate();
        }
        public override void Recalculate()
        {
            base.Recalculate();
            //panelInfo.destination = GetDimensions().ToRectangle();
        }
        public override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimenstions = GetDimensions();
            var rect = dimenstions.ToRectangle();
            if (CalculateBorder)
            {
                rect = Terraria.Utils.CenteredRectangle(rect.Center.ToVector2(), rect.Size() + new Vector2(border * 2));
            }
            #region MagicZone
            /*var graphicDevice = Main.instance.GraphicsDevice;
            SamplerState samplerState = graphicDevice.SamplerStates[0];
            DepthStencilState depthState = graphicDevice.DepthStencilState;
            RasterizerState rasterizerState = graphicDevice.RasterizerState;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, depthState, rasterizerState, null, Main.UIScaleMatrix);
            //spriteBatch.Draw(ModContent.Request<Texture2D>("StoneOfThePhilosophers/UI/ElementPanel").Value, rect, Color.White);
            var panelText = ModContent.Request<Texture2D>("VirtualDream/Contents/StarBound/TimeBackTracking/WindOfTimePanel").Value;
            spriteBatch.Draw(panelText, rect.Center.ToVector2(), null, Color.White, MathHelper.Pi, new Vector2(256), 1f * factor * .5f, 0, 0);
            spriteBatch.Draw(panelText, rect.Center.ToVector2(), null, Color.White * .5f, Main.GlobalTimeWrappedHourly, new Vector2(256), 1.5f * factor * .5f, 0, 0);
            spriteBatch.Draw(panelText, rect.Center.ToVector2(), null, Color.White * .75f, -Main.GlobalTimeWrappedHourly * 2, new Vector2(256), 1.25f * factor * .5f, 0, 0);

            //foreach (var button in Buttons)
            //{

            //    spriteBatch.Draw(panelText, button.GetDimensions().Center(), null, Color.White, Main.GlobalTimeWrappedHourly * .5f, new Vector2(118), .5f * factor, 0, 0);
            //}
            //var elemplr = Main.LocalPlayer.GetModPlayer<ElementPlayer>();
            //spriteBatch.DrawString(FontAssets.MouseText.Value, (elemplr.element1, elemplr.element2).ToString(), Main.LocalPlayer.Center - Main.screenPosition - new Vector2(0, -24), Color.Yellow);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, samplerState, depthState, rasterizerState, null, Main.UIScaleMatrix);*/
            #endregion
            ComplexPanelInfo panelInfo = new();
            panelInfo.destination = Terraria.Utils.CenteredRectangle(Vector2.Lerp(rect.Top(), rect.Center(), factor), rect.Size() * new Vector2(1, factor));
            panelInfo.StyleTexture = ModContent.Request<Texture2D>("VirtualDream/Contents/StarBound/TimeBackTracking/Template_WindOfTime").Value;
            panelInfo.glowEffectColor = Color.Purple with { A = 0 };
            panelInfo.glowShakingStrength = 2f;
            panelInfo.glowHueOffsetRange = 0.2f;

            panelInfo.backgroundTexture = Main.Assets.Request<Texture2D>("Images/UI/HotbarRadial_1").Value;
            panelInfo.backgroundFrame = new Rectangle(4, 4, 28, 28);
            panelInfo.backgroundUnitSize = new Vector2(28, 28) * 2f;
            panelInfo.backgroundColor = Color.Lerp(Color.Purple, Color.Pink, MathF.Sin(Main.GlobalTimeWrappedHourly) * .5f + .5f) * .5f;
            panelInfo.DrawComplexPanel(spriteBatch);
        }
        // 可拖动界面
        private void DragStart(UIMouseEvent evt, UIElement listeningElement)
        {
            // 当点击的是子元素不进行移动
            //Main.NewText((Draggable, evt.Target == this));
            if (Draggable && evt.Target == this)
            {
                Offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
                Dragging = true;
            }
        }

        // 可拖动/调整大小界面
        private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
        {
            Dragging = false;
        }
        public override void OnInitialize()
        {
            WindOfTimeItemSlot windOfTimeItemSlot = windOfTimeSlot = new WindOfTimeItemSlot(new Item());
            windOfTimeItemSlot.Left.Set(4, 0f);
            windOfTimeItemSlot.Top.Set(4, 0f);

            //Elements.Add(windOfTimeItemSlot);
            Append(windOfTimeItemSlot);



        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                //panelInfo.mainColor = KeepOrigin ? CoolerUIPanel.BackgroundDefaultUnselectedColor : Color.White;

            }
            if (Dragging)
            {
                Left.Set(Main.mouseX - Offset.X, 0f);
                Top.Set(Main.mouseY - Offset.Y, 0f);
                Recalculate();
                OnDrag?.Invoke(this);
            }
        }
        public WindOfTimeButton button;
        public WindOfTimeItemSlot windOfTimeSlot;
        public List<WindOfTimeRecipeIcon> recipes;
        public void SetUpElementList()
        {
            Elements.Clear();
            if (recipes == null) recipes = [];
            else recipes.Clear();
            Append(windOfTimeSlot);
            if (windOfTimeSlot._item.type != ModContent.ItemType<WindOfTime>())
            {
                Width.Set(80, 0);
                Height.Set(80, 0);
            }
            else
            {
                if (button == null)
                {
                    WindOfTimeButton _button = button = new WindOfTimeButton(ref currentRecipe);
                    _button.Left.Set(64, 0f);
                    _button.Top.Set(4, 0f);
                    _button.Width.Set(400, 0);
                    _button.Height.Set(48, 0);
                }
                Append(button);


                List<int> types = [];
                Dictionary<int, StarboundWeaponBase> dict = [];
                foreach (var item in Main.LocalPlayer.inventory)
                {
                    if (item.ModItem is StarboundWeaponBase weapon && !types.Contains(item.type))
                    {
                        types.Add(item.type);
                        dict.Add(item.type, weapon);
                    }
                }
                int n = 0;
                float width = 0;
                foreach (var type in types)
                {
                    if (WeaponRepairSystem.Instance.recipes.TryGetValue(type, out var recipe))
                    {
                        var recipeIcon = new WindOfTimeRecipeIcon(recipe);
                        if (recipeIcon.items[0].ModItem is StarboundWeaponBase mainWeapon)
                        {
                            mainWeapon.killCount = dict[type].killCount;
                            mainWeapon.hurtCount = dict[type].hurtCount;
                        }
                        if (recipeIcon.items[^1].ModItem is StarboundWeaponBase result)
                        {
                            result.killCount = dict[type].killCount;
                            result.hurtCount = dict[type].hurtCount;
                        }
                        if (width < recipeIcon.Width.Pixels)
                        {
                            width = recipeIcon.Width.Pixels;
                        }
                        recipeIcon.OnLeftClick += (a, b) =>
                        {
                            if (b is WindOfTimeRecipeIcon recipe)
                            {
                                if (currentRecipe != null)
                                    currentRecipe.active = false;
                                currentRecipe = recipe;
                                button.recipe = recipe;
                                recipe.active = true;
                                SoundEngine.PlaySound(SoundID.Unlock);
                            }
                        };
                        recipeIcon.Left.Set(4, 0);
                        recipeIcon.Top.Set(80 + 48 * n, 0);
                        n++;
                        Append(recipeIcon);
                        recipes.Add(recipeIcon);
                    }
                }
                Width.Set(Math.Max(width + 40, 512), 0);
                //if (n > 5) n = 5;
                Height.Set(100 + 48 * n, 0);
            }

            Recalculate();
        }  
        public event ElementEvent OnDrag;
    }
    public class WindOfTimeItemSlot : UIElement
    {
        public Item _item;
        /// <summary>
        /// 0-1为物品框展开
        /// 1-2为物品框展开光效
        /// </summary>
        public float factor;
        public WindOfTimeItemSlot(Item item)
        {
            _item = item;
            Width = new StyleDimension(48f, 0f);
            Height = new StyleDimension(48f, 0f);
        }

        private void HandleItemSlotLogic()
        {
            if (IsMouseHovering && factor > 1)
            {
                Main.LocalPlayer.mouseInterface = true;
                Item inv = _item;
                ItemSlot.OverrideHover(ref inv);
                ItemSlot.LeftClick(ref inv);
                ItemSlot.RightClick(ref inv);
                ItemSlot.MouseHover(ref inv);

                _item = inv;
            }
        }

       public override void DrawSelf(SpriteBatch spriteBatch)
        {
            HandleItemSlotLogic();
            Item inv = _item;
            Vector2 position = GetDimensions().Center() + new Vector2(52f, 52f) * -0.5f * Main.inventoryScale;
            var rect = GetDimensions().ToRectangle();
            var _factor = factor;
            _factor = MathHelper.Clamp(_factor, 0, 1);
            ComplexPanelInfo panelInfo = new();
            panelInfo.destination = Terraria.Utils.CenteredRectangle(Vector2.Lerp(rect.Top(), rect.Center(), _factor), rect.Size() * new Vector2(1, _factor));
            panelInfo.StyleTexture = ModContent.Request<Texture2D>("VirtualDream/Contents/StarBound/TimeBackTracking/Template_WindOfTime").Value;
            if (inv.type == ModContent.ItemType<WindOfTime>())
            {
                panelInfo.glowEffectColor = Color.Purple with { A = 0 };
                panelInfo.glowShakingStrength = 2f;
                panelInfo.glowHueOffsetRange = 0.2f;
                panelInfo.backgroundTexture = Main.Assets.Request<Texture2D>("Images/UI/HotbarRadial_1").Value;
                panelInfo.backgroundFrame = new Rectangle(4, 4, 28, 28);
                panelInfo.backgroundUnitSize = new Vector2(28, 28) * 2f;
                panelInfo.backgroundColor = Color.Lerp(Color.Purple, Color.Pink, MathF.Sin(Main.GlobalTimeWrappedHourly) * .5f + .5f) with { A = 0 } * .5f;
            }

            panelInfo.DrawComplexPanel(spriteBatch);
            if (factor > 1)
            {
                //if (factor > 1.5f)
                ItemSlot.DrawItemIcon(inv, 31, spriteBatch, position + new Vector2(16), 1f, 32f, Color.White * (factor - 1));
                //ItemSlot.Draw(spriteBatch, ref inv, 0, position);
                //#region MagicZone
                //var factor2 = factor - 1;
                //var factor3 = factor2.HillFactor2();
                //var graphicDevice = Main.instance.GraphicsDevice;
                //SamplerState samplerState = graphicDevice.SamplerStates[0];
                //DepthStencilState depthState = graphicDevice.DepthStencilState;
                //RasterizerState rasterizerState = graphicDevice.RasterizerState;
                //spriteBatch.End();
                //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, depthState, rasterizerState, null, Main.UIScaleMatrix);
                ////spriteBatch.Draw(ModContent.Request<Texture2D>("StoneOfThePhilosophers/UI/ElementPanel").Value, rect, Color.White);
                //var panelText = ModContent.Request<Texture2D>("VirtualDream/Contents/StarBound/TimeBackTracking/WindOfTimePanel").Value;
                //spriteBatch.Draw(panelText, rect.Center.ToVector2(), null, Color.White * factor3, MathHelper.Pi, new Vector2(256), 1f * factor2 * .5f, 0, 0);
                //spriteBatch.Draw(panelText, rect.Center.ToVector2(), null, Color.White * .5f * factor3, Main.GlobalTimeWrappedHourly, new Vector2(256), 1.5f * factor2 * .5f, 0, 0);
                //spriteBatch.Draw(panelText, rect.Center.ToVector2(), null, Color.White * .75f * factor3, -Main.GlobalTimeWrappedHourly * 2, new Vector2(256), 1.25f * factor2 * .5f, 0, 0);

                ////foreach (var button in Buttons)
                ////{

                ////    spriteBatch.Draw(panelText, button.GetDimensions().Center(), null, Color.White, Main.GlobalTimeWrappedHourly * .5f, new Vector2(118), .5f * factor, 0, 0);
                ////}
                ////var elemplr = Main.LocalPlayer.GetModPlayer<ElementPlayer>();
                ////spriteBatch.DrawString(FontAssets.MouseText.Value, (elemplr.element1, elemplr.element2).ToString(), Main.LocalPlayer.Center - Main.screenPosition - new Vector2(0, -24), Color.Yellow);

                //spriteBatch.End();
                //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, samplerState, depthState, rasterizerState, null, Main.UIScaleMatrix);
                //#endregion


                if (IsMouseHovering && inv.type != ModContent.ItemType<WindOfTime>())
                {
                    ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, "请放入<正确>的物品以激活基座", Main.MouseScreen + Main.rand.NextVector2Unit(), Color.Purple, 0, default, new Vector2(1f), 100, Main.rand.NextFloat(2, 4));
                }

            }
            _factor = factor.SymmetricalFactor(1, 1);
            panelInfo = new ComplexPanelInfo();
            panelInfo.StyleTexture = ModContent.Request<Texture2D>("VirtualDream/Contents/StarBound/TimeBackTracking/Template_WindOfTime").Value;
            panelInfo.destination = Terraria.Utils.CenteredRectangle(Vector2.Lerp(rect.Top(), rect.Center(), _factor), rect.Size() * new Vector2(1, _factor));
            panelInfo.DrawComplexPanel(spriteBatch);
        }
    }
    public enum RepairRecipeState
    {
        /// <summary>
        /// 两个都不够
        /// </summary>
        Both,
        /// <summary>
        /// 主武器未达成修复/升级条件
        /// </summary>
        MainWeaponUnavailable,
        /// <summary>
        /// 原料缺少
        /// </summary>
        LackOfIngredient,
        JustDoIt
    }
    public class WindOfTimeRecipeIcon : UIElement
    {
        public bool active;
        public WeaponRepairRecipe recipe;
        public float factor;
        public float factor2;
        public Item[] items;
        public RepairRecipeState state;
        public WindOfTimeRecipeIcon(WeaponRepairRecipe repairRecipe)
        {
            recipe = repairRecipe;
            items = new Item[recipe.ingredients.Count + 2];
            items[0] = recipe.MainWeapon.Item.Clone();
            for (int i = 1; i < items.Length - 1; i++)
                items[i] = recipe.ingredients[i - 1].Clone();
            items[^1] = new Item(recipe.ResultType);
            Width.Set(32 + 96 + repairRecipe.ingredients.Count * 48, 0f);
            Height.Set(32f, 0f);
        }
        private void HandleItemSlotLogic(int index)
        {
            if (IsMouseHovering && factor > 1 && index != -1)
            {
                Main.LocalPlayer.mouseInterface = true;
                Item inv = items[index];
                ItemSlot.OverrideHover(ref inv);
                //ItemSlot.LeftClick(ref inv);
                //ItemSlot.RightClick(ref inv);
                ItemSlot.MouseHover(ref inv);
                items[index] = inv;
            }
        }
        public override void DrawSelf(SpriteBatch spriteBatch)
        {
            int _state = 0;
            #region 底板
            ComplexPanelInfo panelInfo = new();
            panelInfo.destination = GetDimensions().ToRectangle();
            panelInfo.destination.Height = (int)(panelInfo.destination.Height * MathHelper.Clamp(factor, 0, 1));
            panelInfo.StyleTexture = ModContent.Request<Texture2D>("VirtualDream/Contents/StarBound/TimeBackTracking/Template_WindOfTime").Value;
            factor2 = MathHelper.Lerp(factor2, active ? 1 : 0, 0.2f);
            panelInfo.glowEffectColor = Color.Purple with { A = 0 } * factor2;
            panelInfo.glowShakingStrength = 2f * factor2;
            panelInfo.glowHueOffsetRange = 0.2f * factor2;

            panelInfo.destination.X += 128;
            panelInfo.destination.Width -= 128;
            panelInfo.DrawComplexPanel(spriteBatch);

            panelInfo.destination.X -= 128;
            panelInfo.destination.Width = 32;
            panelInfo.DrawComplexPanel(spriteBatch);

            panelInfo.destination.X += 80;
            panelInfo.DrawComplexPanel(spriteBatch);
            #endregion
            #region 物品
            if (factor > 1)
            {
                int index = -1;
                Vector2 left = GetDimensions().ToRectangle().Left();
                Point mouseScreen = UIMethods.TransformToUIPosition(Main.MouseScreen).ToPoint();
                Rectangle rectangle = new((int)left.X, (int)left.Y - 16, 32, 32);
                if (rectangle.Contains(mouseScreen))
                    index = 0;
                ItemSlot.DrawItemIcon(recipe.MainWeapon.Item, 31, spriteBatch, left + new Vector2(16, 0), recipe.MainWeapon.Item.scale, 32f, Color.White);
                rectangle.X += 80;
                if (rectangle.Contains(mouseScreen))
                    index = items.Length - 1;
                ItemSlot.DrawItemIcon(new Item(recipe.ResultType), 31, spriteBatch, left + new Vector2(96, 0), recipe.MainWeapon.Item.scale, 32f, Color.White);
                spriteBatch.Draw(ModContent.Request<Texture2D>("VirtualDream/Contents/StarBound/TimeBackTracking/timeArrow").Value, left + new Vector2(36, -4), Color.White);
                var infos = from item in recipe.ingredients where true select (item.type, item.stack);
                var types = from info in infos where true select info.type;
                Dictionary<int, (int, int)> dict = [];
                foreach (var info in infos)
                {
                    dict.Add(info.type, (info.stack, 0));
                }
                bool found = false;
                foreach (var item in Main.LocalPlayer.inventory)
                {
                    if (types.Contains(item.type) && dict.TryGetValue(item.type, out var value))
                    {
                        dict[item.type] = (value.Item1, value.Item2 + item.stack);
                    }
                    if (!found && item.type == recipe.MainWeapon.Type)
                    {
                        found = true;
                        if (item.ModItem is StarboundWeaponBase weaponBase && weaponBase.UpgradeAvailable)
                            _state += 2;
                    }
                }
                var font = FontAssets.MouseText.Value;
                rectangle.X += 56;
                bool ingredientsEnough = true;
                for (int n = 0; n < recipe.ingredients.Count; n++)
                {
                    var _item = recipe.ingredients[n];
                    //ItemSlot.Draw(spriteBatch, ref _item, 0, left + new Vector2(64 + n * 32, 0));
                    if (rectangle.Contains(mouseScreen))
                        index = n + 1;
                    ItemSlot.DrawItemIcon(_item, 31, spriteBatch, left + new Vector2(152 + n * 48, 0), _item.scale, 32f, Color.White);
                    if (dict.TryGetValue(_item.type, out var value))
                    {
                        var str = $"{value.Item2}/{value.Item1}";

                        //UIMethods.DrawMouseTextOnRectangle($"[c:FF0000/{value.Item2}] / {value.Item1}", Color.White, Color.Black, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                        //- (int)font.MeasureString(str).X / 8
                        ingredientsEnough &= value.Item2 >= value.Item1;
                        Terraria.Utils.DrawBorderStringFourWay(Main.spriteBatch, font, str, rectangle.X, rectangle.Y + 16, value.Item2 >= value.Item1 ? Color.MediumPurple : Color.White, Color.Black, Vector2.Zero, 0.75f);
                    }
                    rectangle.X += 48;
                }
                //Main.NewText((ingredientsEnough, recipe.MainWeapon.DisplayName));
                if (ingredientsEnough)
                    _state += 1;
                HandleItemSlotLogic(index);
            }
            #endregion
            #region 底板二号
            ComplexPanelInfo panelInfo2 = new();
            panelInfo2.destination = GetDimensions().ToRectangle();
            panelInfo2.StyleTexture = ModContent.Request<Texture2D>("VirtualDream/Contents/StarBound/TimeBackTracking/Template_WindOfTime").Value;
            panelInfo2.destination.Height = (int)(panelInfo2.destination.Height * factor.SymmetricalFactor(1, 1));

            panelInfo2.destination.X += 128;
            panelInfo2.destination.Width -= 128;
            panelInfo2.DrawComplexPanel(spriteBatch);

            panelInfo2.destination.X -= 128;
            panelInfo2.destination.Width = 32;
            panelInfo2.DrawComplexPanel(spriteBatch);

            panelInfo2.destination.X += 80;
            panelInfo2.DrawComplexPanel(spriteBatch);
            #endregion
            state = (RepairRecipeState)_state;
        }
    }
    public class WindOfTimeButton : UIElement
    {
        public WindOfTimeRecipeIcon recipe;
        public float factor;
        public WindOfTimeButton(ref WindOfTimeRecipeIcon repairRecipe)
        {
            recipe = repairRecipe;
        }
        public override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimenstions = GetDimensions();
            var rect = dimenstions.ToRectangle();
            ComplexPanelInfo panelInfo = new();
            var _factor = MathHelper.Clamp(factor, 0, 1);
            panelInfo.destination = Terraria.Utils.CenteredRectangle(Vector2.Lerp(rect.Top(), rect.Center(), _factor), rect.Size() * new Vector2(1, _factor));
            panelInfo.StyleTexture = ModContent.Request<Texture2D>("VirtualDream/Contents/StarBound/TimeBackTracking/Template_WindOfTime").Value;
            if (recipe != null && recipe.state == RepairRecipeState.JustDoIt)
            {
                panelInfo.glowEffectColor = Color.Purple with { A = 0 };
                panelInfo.glowShakingStrength = 2f;
                panelInfo.glowHueOffsetRange = 0.2f;
            }
            panelInfo.backgroundTexture = Main.Assets.Request<Texture2D>("Images/UI/HotbarRadial_1").Value;
            panelInfo.backgroundFrame = new Rectangle(4, 4, 28, 28);
            panelInfo.backgroundUnitSize = new Vector2(28, 28) * 2f;
            panelInfo.backgroundColor = Color.Lerp(Color.Purple, Color.Pink, MathF.Sin(Main.GlobalTimeWrappedHourly) * .5f + .5f) * .5f;
            panelInfo.DrawComplexPanel(spriteBatch);
            if (factor > 1)
            {
                var str = recipe != null ? recipe.state switch
                {
                    RepairRecipeState.Both => "武器未达成条件、原料不足",
                    RepairRecipeState.MainWeaponUnavailable => "武器未达成条件，再去带着它战斗一下吧",
                    RepairRecipeState.LackOfIngredient => "原料不足，你还需要一点准备工作",
                    RepairRecipeState.JustDoIt or _ => "淦就完了，武器修复，启动！"
                } : "未选中合成";
                var font = FontAssets.MouseText.Value;
                Vector2 vec = font.MeasureString(str);
                bool available = recipe?.state == RepairRecipeState.JustDoIt;
                ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font, str, rect.Center() + (available ? Main.rand.NextVector2Unit() : default), (available ? (IsMouseHovering ? Color.MediumPurple : Color.Purple) : (IsMouseHovering ? Color.White : Color.Gray)), 0, vec * .5f, Vector2.One * new Vector2(1, factor - 1));
            }

            base.DrawSelf(spriteBatch);
        }
        public override void LeftClick(UIMouseEvent evt)
        {
            if (recipe == null)
            {
                Main.NewText("未选中合成", Color.MediumPurple);
            }
            else
            {
                if (recipe.state == RepairRecipeState.JustDoIt)
                {

                    SoundEngine.PlaySound(SoundID.Zombie104);
                    //foreach (var item in recipe.recipe.ingredients)
                    //{
                    //    for (int n = 0; n < item.stack; n++)
                    //        Main.LocalPlayer.ConsumeItem(item.type);
                    //}
                    //Main.LocalPlayer.ConsumeItem(recipe.recipe.MainWeapon.Type);
                    var point = WeaponRepairSystem.Instance.repairUI.standData.tilePosition;
                    var proj = Projectile.NewProjectileDirect(Main.LocalPlayer.GetSource_TileInteraction(point.X, point.Y), point.ToWorldCoordinates() + new Vector2(-24, -64), default, ModContent.ProjectileType<WindOfTimeReactionProj>(), 0, 0, Main.myPlayer);
                    (proj.ModProjectile as WindOfTimeReactionProj).recipeItemInfo = (Item[])recipe.items.Clone();

                    WeaponRepairSystem.Instance.repairUI.Close();

                }
                else
                {
                    Main.NewText("现在还不是时候", Color.MediumPurple);
                }
            }

            base.LeftClick(evt);
        }
    }
    public class WindOfTimeReactionProj : ModProjectile
    {
        public Item[] recipeItemInfo;
        public override void SetDefaults()
        {
            Projectile.timeLeft = 180;

            base.SetDefaults();
        }
        public override void AI()
        {
            //Dust.NewDustPerfect(Projectile.Center, DustID.PurpleTorch, Main.rand.NextVector2Unit());//糊弄一下(乐
            if (Projectile.timeLeft == 1)
            {
                var i = Item.NewItem(Projectile.GetSource_GiftOrReward(), Projectile.Center, recipeItemInfo[^1].Clone());
                //var result = Main.item[i];
                //if (result.ModItem is StarboundWeaponBase weaponBase)
                //{
                //    Main.NewText((recipe.MainWeapon.killCount, recipe.MainWeapon.hurtCount));
                //    (weaponBase.killCount, weaponBase.hurtCount) = (recipe.MainWeapon.killCount, recipe.MainWeapon.hurtCount);
                //}
            }
            base.AI();
        }
        public override string Texture => "Terraria/Images/Item_0";
        public override bool PreDraw(ref Color lightColor)
        {
            //if (Main.gamePaused) Projectile.Update(Projectile.whoAmI);
            var spb = Main.spriteBatch;
            var factor = Terraria.Utils.GetLerpValue(0, 180, Projectile.timeLeft);
            spb.End();
            spb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, LogSpiralLibraryMod.FadeEffect, Main.GameViewMatrix.TransformationMatrix);
            LogSpiralLibraryMod.FadeEffect.Parameters["uOffset"].SetValue(factor);
            Main.instance.GraphicsDevice.Textures[1] = LogSpiralLibraryMod.BaseTex[9].Value;
            (DrawData, float)[] drawDatas = new (DrawData, float)[recipeItemInfo.Length - 1];
            var _cen = Projectile.Center - Main.screenPosition;
            for (int i = 0; i < drawDatas.Length; i++)
            {
                var texture = TextureAssets.Item[recipeItemInfo[i].type].Value;
                var offset = i == 0 ? default : ((factor + (i - 1f) / (drawDatas.Length - 1f)) * MathHelper.TwoPi).ToRotationVector2();
                drawDatas[i] = (new DrawData(texture, new Vector2(128, 96) * offset * factor + _cen, null, lightColor, 0, texture.Size() * .5f, 1f, 0), offset.Y);
            }
            var newData = drawDatas.OrderBy(data => data.Item2);
            foreach (var data in newData)
            {
                data.Item1.Draw(spb);
            }
            spb.End();
            spb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
    }
    public class WindOfTimeStandTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true;
            TileID.Sets.HasOutlines[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Origin = new Point16(2, 1);
            TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 16 };
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<WindOfTimeTileEntity>().Hook_AfterPlacement, -1, 0, false);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(28, 28, 28), Language.GetText("VirtualDream.Items.WindOfTimeStand.DisplayName"));
            DustType = MyDustId.PurpleLight;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            ModContent.GetInstance<WindOfTimeTileEntity>().Kill(i, j);
            //Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<WindOfTimeStand>());
        }
        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
        {
            return true;
        }
        public override bool RightClick(int i, int j)
        {
            if (TileMethods.TryGetTileEntityAs<WindOfTimeTileEntity>(i, j, out var entity))
            {
                if (entity.data == null)
                {
                    Main.NewText(entity.GetHashCode(), Color.Red);
                }
                else
                {
                    if (!WeaponRepairUI.Visible)
                        WeaponRepairSystem.Instance.repairUI.Open(entity.data);
                    else
                        WeaponRepairSystem.Instance.repairUI.Close();
                }

            }
            else
            {
                //Main.NewText("WTH");

                //Main.NewText(TileEntity.ByPosition.TryGetValue(TileMethods.GetTopLeftTileInMultitile(i,j), out TileEntity _));
            }
            return false;
        }
        public override void MouseOver(int i, int j)
        {
            Player localPlayer = Main.LocalPlayer;
            localPlayer.noThrow = 2;
            localPlayer.cursorItemIconEnabled = true;
            localPlayer.cursorItemIconID = ModContent.ItemType<WindOfTime>();
            base.MouseOver(i, j);
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            base.DrawEffects(i, j, spriteBatch, ref drawData);
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {

            if (TileMethods.GetTopLeftTileInMultitile(i, j) == new Point16(i, j) && TileMethods.TryGetTileEntityAs<WindOfTimeTileEntity>(i, j, out var entity) && entity.data.item.type != 0)
            {
                Vector2 zero = new(Main.offScreenRange, Main.offScreenRange);
                if (Main.drawToScreen)
                {
                    zero = Vector2.Zero;
                }
                Color color = Lighting.GetColor(i, j);
                Vector2 position = new Point(i + 2, j + 1).ToWorldCoordinates() + new Vector2(0, -64) - Main.screenPosition + zero + new Vector2(0, MathF.Cos((float)LogSpiralLibraryMod.ModTime / 60f) * 16);
                #region MagicZone
                if (entity.data.item.type == ModContent.ItemType<WindOfTime>())
                {
                    var factor2 = 0.5f;
                    var graphicDevice = Main.instance.GraphicsDevice;
                    SamplerState samplerState = graphicDevice.SamplerStates[0];
                    DepthStencilState depthState = graphicDevice.DepthStencilState;
                    RasterizerState rasterizerState = graphicDevice.RasterizerState;

                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, depthState, rasterizerState, null, Matrix.Identity);
                    //spriteBatch.Draw(ModContent.Request<Texture2D>("StoneOfThePhilosophers/UI/ElementPanel").Value, rect, Color.White);
                    var panelText = ModContent.Request<Texture2D>("VirtualDream/Contents/StarBound/TimeBackTracking/WindOfTimePanel").Value;
                    spriteBatch.Draw(panelText, position, null, color, MathHelper.Pi, new Vector2(256), 1f * factor2 * .5f, 0, 0);
                    spriteBatch.Draw(panelText, position, null, color * .5f, (float)LogSpiralLibraryMod.ModTime / 60f, new Vector2(256), 1.5f * factor2 * .5f, 0, 0);
                    spriteBatch.Draw(panelText, position, null, color * .75f, -(float)LogSpiralLibraryMod.ModTime / 30f, new Vector2(256), 1.25f * factor2 * .5f, 0, 0);
                    #region GiveUP
                    //foreach (var button in Buttons)
                    //{

                    //    spriteBatch.Draw(panelText, button.GetDimensions().Center(), null, Color.White, Main.GlobalTimeWrappedHourly * .5f, new Vector2(118), .5f * factor, 0, 0);
                    //}
                    //var elemplr = Main.LocalPlayer.GetModPlayer<ElementPlayer>();
                    //spriteBatch.DrawString(FontAssets.MouseText.Value, (elemplr.element1, elemplr.element2).ToString(), Main.LocalPlayer.Center - Main.screenPosition - new Vector2(0, -24), Color.Yellow);

                    //var trans = Main.GameViewMatrix != null ? Main.GameViewMatrix.TransformationMatrix : Matrix.Identity;

                    //spriteBatch.End();
                    //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone, null, trans);//Main.DefaultSamplerState//Main.GameViewMatrix.TransformationMatrix

                    //LogSpiralLibraryMod.TransformEffectEX.Parameters["factor1"].SetValue(0.5f);
                    //LogSpiralLibraryMod.TransformEffectEX.Parameters["factor2"].SetValue((float)LogSpiralLibraryMod.ModTime / 30f);
                    //LogSpiralLibraryMod.TransformEffectEX.CurrentTechnique.Passes[7].Apply();
                    //spriteBatch.Draw(TextureAssets.Projectile[ModContent.ProjectileType<OculusReaverTear>()].Value, position, null, Color.Purple, 0, new Vector2(512), 2f * 46 / 512 * new Vector2(1, 1), 0, 0);//new Rectangle(240,240,92,92)
                    #endregion
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, samplerState, depthState, rasterizerState, null, Matrix.Identity);

                }

                #endregion
                Main.DrawItemIcon(spriteBatch, entity.data.item, position, color, 144);
            }

            return base.PreDraw(i, j, spriteBatch);
        }
    }
    public class WindOfTimeTileEntity : LModTileEntity<WindOfTimeStandTile>
    {
        public override Point16 Origin => new(2, 1);
        public WindOfTimeStandData data;
        public bool Active => data != null && data.item.type == ModContent.ItemType<WindOfTime>();
        public override void SaveData(TagCompound tag)
        {
            tag.Add("tilePosition", data.tilePosition.ToVector2());
            tag.Add("item", ItemIO.Save(data.item));
        }
        public override void LoadData(TagCompound tag)
        {
            data = new WindOfTimeStandData(tag.Get<Vector2>("tilePosition").ToPoint(), ItemIO.Load(tag.GetCompound("item")));
        }
        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
        {
            int n = base.Hook_AfterPlacement(i, j, type, style, direction, alternate);
            if (TileMethods.TryGetTileEntityAs(i, j, out WindOfTimeTileEntity entity))
                entity.data = new WindOfTimeStandData(new Point(i + Origin.X, j + Origin.Y), new Item());
            return n;
        }
    }
    public class WindOfTimeStandData
    {
        public Point tilePosition;
        public Item item;
        public WindOfTimeStandData(Point position, Item _item)
        {
            tilePosition = position;
            item = _item;
        }
    }
}
