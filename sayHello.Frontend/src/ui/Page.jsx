function Page({children}) {
  return (
    <div className={StyledContainer}style={{ backgroundImage: "url('./bg.jpg')" }}>
    <div className="absolute inset-0 bg-gradient-bg opacity-80 "></div> 
    <div className="relative z-10 grid px-10 lg:grid-cols-2 sm:grid-rows-2 lg:gap-40 sm:gap-0 items-center">
      {children}
    </div>
    </div>
  )
}
const StyledContainer ="relative  bg-cover bg-no-repeat bg-center";
export default Page