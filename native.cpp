typedef unsigned int HRESULT;

const HRESULT S_OK = 0;
const HRESULT E_FAIL = 0x80004005;


class IUnknown {
public:
	virtual HRESULT QueryInterface (void *riid, void **obj) = 0;
	virtual unsigned int AddRef () = 0;
	virtual unsigned int Release () = 0;
		
};

class TestImpl : IUnknown
{
public:
	/* IUnknown methods */
	
	HRESULT QueryInterface (void *riid, void **obj)
	{
		*obj = this;
		return S_OK;
	};

	unsigned int AddRef ()
	{
		return 1;
	};

	unsigned int Release ()
	{
		return S_OK;
	};

	/* TestImpl methods */

	virtual HRESULT Test_Preserved(int *retVal)
	{
		*retVal = 42;
		return S_OK;
	}

	virtual HRESULT Test(int *retVal)
	{
		*retVal = 17;
		return S_OK;
	}

	virtual HRESULT Test_Fail()
	{
		return E_FAIL;
	}
};



extern "C" TestImpl *getTestImpl ()
{
	return new TestImpl ();
}
