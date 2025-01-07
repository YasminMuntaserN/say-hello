function LoadingChattingCards() {
  const arr = Array.from({ length: 4 }, (_, i) => i + 1);

  return (
    <div>
      {arr.map((index) => (
        <div key={index} className="border-2 border-zinc-500 m-5 p-2">
          <div className="w-10 h-10 rounded-full bg-lightText"></div>
          <div className="ml-10">
            <div className="w-full rounded-lg bg-lightText h-4"></div>
            <div className="w-1/2 rounded-lg bg-lightText h-4 mt-2"></div>
          </div>
        </div>
      ))}
    </div>
  );
}

export default LoadingChattingCards;