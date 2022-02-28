﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Alpaca.Markets
{
    [SuppressMessage(
        "Microsoft.Performance", "CA1812:Avoid uninstantiated internal classes",
        Justification = "Object instances of this class will be created by Newtonsoft.JSON library.")]
    internal sealed class JsonSnapshot : ISnapshot, ISymbolMutable
    {
        [JsonProperty(PropertyName = "latestQuote", Required = Required.Default)]
        public JsonHistoricalQuote? JsonQuote { get; set; } = new();

        [JsonProperty(PropertyName = "latestTrade", Required = Required.Default)]
        public JsonHistoricalTrade? JsonTrade { get; set; } = new();

        [JsonProperty(PropertyName = "minuteBar", Required = Required.Default)]
        public JsonHistoricalBar? JsonMinuteBar { get; set; }

        [JsonProperty(PropertyName = "dailyBar", Required = Required.Default)]
        public JsonHistoricalBar? JsonCurrentDailyBar { get; set; }

        [JsonProperty(PropertyName = "prevDailyBar", Required = Required.Default)]
        public JsonHistoricalBar? JsonPreviousDailyBar { get; set; }

        [JsonProperty(PropertyName = "symbol", Required = Required.Default)]
        public String Symbol { get; set; } = String.Empty;

        [JsonIgnore]
        public IQuote? Quote => JsonQuote;

        [JsonIgnore]
        public ITrade? Trade => JsonTrade;

        [JsonIgnore]
        public IBar? MinuteBar => JsonMinuteBar;

        [JsonIgnore]
        public IBar? CurrentDailyBar => JsonCurrentDailyBar;

        [JsonIgnore]
        public IBar? PreviousDailyBar => JsonPreviousDailyBar;

        [OnDeserialized]
        [UsedImplicitly]
        internal void OnDeserializedMethod(
            StreamingContext _) =>
            SetSymbol(Symbol);

        public void SetSymbol(
            String symbol)
        {
            Symbol = symbol;
            JsonTrade?.SetSymbol(Symbol);
            JsonQuote?.SetSymbol(Symbol);
            JsonMinuteBar?.SetSymbol(Symbol);
            JsonCurrentDailyBar?.SetSymbol(Symbol);
            JsonPreviousDailyBar?.SetSymbol(Symbol);
        }
    }
}
