function Box({ children, colsNum ,HandleOnClick }) {
  const gridClasses = {
    2: "grid-cols-2 gap-0",
    3: "grid-cols-[1fr_2fr_1fr] gap-10",
  };

  const StyledContainer = `group grid ${gridClasses[colsNum]} hover:bg-[#30323d]  m-5 p-3 rounded-2xl shadow-2xl shadow-slate-500 transition-all`;

  return (
    <div className={StyledContainer} onClick={HandleOnClick}>
      {children}
    </div>
  );
}

export default Box;
