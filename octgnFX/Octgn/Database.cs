// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Database.cs" company="OCTGN">
//   GNU Stuff
// </copyright>
// <summary>
//   Defines the Database type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Octgn
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Octgn.Data;
    using Octgn.Definitions;

    /// <summary>
    /// The TEMPORARY database class.
    /// </summary>
    public static class Database
    {
        /// <summary>
        /// Gets the opened game.
        /// </summary>
        public static Data.Game OpenedGame { get; private set; }

        /// <summary>
        /// Open a game.
        /// </summary>
        /// <param name="game">
        /// The game.
        /// </param>
        /// <param name="readOnly">
        /// The read only.
        /// </param>
        public static void Open(GameDef game, bool readOnly)
        {
            OpenedGame = Program.GamesRepository.Games.First(g => g.Id == game.Id);
        }

        /// <summary>
        /// Close the open game. Doesn't do anything currently.
        /// </summary>
        /// <exception cref="NotImplementedException">Doesn't do shit.</exception>
        public static void Close()
        {
            throw new NotImplementedException("Doesn't do shit.");
        }

        /// <summary>
        /// Get a card by name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="CardModel"/>.
        /// </returns>
        public static CardModel GetCardByName(string name)
        {
            return OpenedGame.GetCardByName(name);
        }

        /// <summary>
        /// Get a card by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="CardModel"/>.
        /// </returns>
        public static CardModel GetCardById(Guid id)
        {
            return OpenedGame.GetCardById(id);
        }

        /// <summary>
        /// Get all markers for this game.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<MarkerModel> GetAllMarkers()
        {
            return OpenedGame.GetAllMarkers();
        }

        /// <summary>
        /// Get cards from the game using undocumented conditions.
        /// There is no proper way to tell what the hell this does without digging into it.
        /// <see cref="Data.Game.SelectCardModels"/>
        /// </summary>
        /// <param name="conditions">
        /// The scary unknown conditions.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<CardModel> GetCards(params string[] conditions)
        {
            return OpenedGame.SelectCardModels(conditions);
        }

        /// <summary>
        /// Get all the sets for the opened game.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<Set> GetAllSets()
        {
            return OpenedGame.Sets;
        }

        /// <summary>
        /// Get a pack by id.
        /// </summary>
        /// <param name="packId">
        /// The pack id.
        /// </param>
        /// <returns>
        /// The <see cref="Pack"/>.
        /// </returns>
        public static Pack GetPackById(Guid packId)
        {
            return OpenedGame.GetPackById(packId);
        }
    }
}