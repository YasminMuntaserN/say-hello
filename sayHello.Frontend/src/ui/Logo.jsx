function Logo({ size = ""  ,color ="white"}) {
  return (
    <div>
      {color =="white"?
        <img className={`inline-block ${size}`} src="/logo.png" alt="logo" />
        :<img className={`inline-block ${size}`} src="/black_logo.png" alt="logo" /> 
      }
    </div>
  );
}

export default Logo;

