function Image({src ,size}) {
const StyledImg =`rounded-full ${size ?size: "h-12 w-12"}`;
  return (
      <img src={`https://localhost:7201${src}`} alt="user" className={StyledImg} />
  )
}
export default Image
