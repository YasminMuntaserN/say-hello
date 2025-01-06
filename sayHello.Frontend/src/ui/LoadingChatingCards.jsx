function LoadingChattingCards() {
  const arr =Array.from({ length: 3 }, (_, i) => i + 1);
  return (
    {arr.map((index)=>(
    <div key={index} className="bg-dark-50">
        <div className="w-10 h-10 rounded-full bg-lightText"></div>

        <div className="ml-[-50px]  text-center">
            <div className="w-full bg-lightText"></div>
            <div className="w-[1/2] rounded-full bg-lightText"></div>
        </div>
    
    </div>
  ))}
  )
}

export default LoadingChattingCards
