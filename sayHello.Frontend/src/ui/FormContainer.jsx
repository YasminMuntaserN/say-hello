function FormContainer({ header , children}) {
  return (
    <div className={StyledContainer}>
      <h1 className={StyledH}>{header}</h1>
      {children}
    </div>
  )
}

export default FormContainer


const StyledContainer = "bg-white z-1000 rounded-3xl shadow-lg mx-auto mt-10  sm:w-[150%] lg:w-3/4 md:w-[100%] lg:ml-auto p-10";
const StyledH = "text-3xl font-bold mb-5 text-basic overflow-hidden";