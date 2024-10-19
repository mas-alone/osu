// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Game.Tournament.Models;
using osuTK;

namespace osu.Game.Tournament.Components
{
    public partial class DrawableTeamFlag : Container
    {
        private readonly TournamentTeam? team;

        [UsedImplicitly]
        private Bindable<string>? flag;

        private Sprite? flagSprite;

        public DrawableTeamFlag(TournamentTeam? team)
        {
            this.team = team;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            if (team == null) return;
            Masking = true;
            CornerRadius = 5;

            if (team.Players.Count == 1)
            {

                Size = new Vector2(75, 75);

                int uID = team.Players[0].OnlineID;
                string avatarUrl = $@"https://a.ppy.sh/{uID}";

                Child = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    FillMode = FillMode.Fit,
                    Texture = textures.Get(avatarUrl),
                };
            }
            else
            {
                Size = new Vector2(75, 54);

                Child = flagSprite = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    FillMode = FillMode.Fill  // ÆìÖÄÊÊÓ¦ÈÝÆ÷´óÐ¡
                };

                (flag = team.FlagName.GetBoundCopy()).BindValueChanged(_ => flagSprite.Texture = textures.Get($@"Flags/{team.FlagName}"), true);
            }
        }
    }
}
