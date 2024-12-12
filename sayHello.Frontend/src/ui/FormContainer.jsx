function FormContainer({ header , children}) {
  return (
    <div className={StyledContainer}>
      <h1 className={StyledH}>{header}</h1>
      {children}
    </div>
  )
}

export default FormContainer


const StyledContainer = "bg-white z-1000 rounded-3xl shadow-lg mx-auto mt-10 w-3/4 h-max  p-10 overflow-hidden";
const StyledH = "text-3xl font-bold mb-5 text-basic overflow-hidden";