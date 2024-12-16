function Image({src}) {
  console.log(`https://localhost:7201${src}`);
  return (
      <img src={`https://localhost:7201${src}`} alt="user" className={StyledImg} />
  )
}
const StyledImg ="rounded-full h-10 w-10 mt-[-10px]";
export default Image
